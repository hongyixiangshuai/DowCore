namespace Dow.Core.Controllers
{
    using Abp.Authorization;
    using Abp.Authorization.Users;
    using Abp.MultiTenancy;
    using Abp.Runtime.Security;
    using Abp.UI;
    using Dow.Core.Authentication.External;
    using Dow.Core.Authentication.JwtBearer;
    using Dow.Core.Authorization;
    using Dow.Core.Authorization.Users;
    using Dow.Core.Models.TokenAuth;
    using Dow.Core.MultiTenancy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TokenAuthController" />
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : CoreControllerBase
    {
        /// <summary>
        /// Defines the _logInManager
        /// </summary>
        private readonly LogInManager _logInManager;

        /// <summary>
        /// Defines the _tenantCache
        /// </summary>
        private readonly ITenantCache _tenantCache;

        /// <summary>
        /// Defines the _abpLoginResultTypeHelper
        /// </summary>
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;

        /// <summary>
        /// Defines the _configuration
        /// </summary>
        private readonly TokenAuthConfiguration _configuration;

        /// <summary>
        /// Defines the _externalAuthConfiguration
        /// </summary>
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;

        /// <summary>
        /// Defines the _externalAuthManager
        /// </summary>
        private readonly IExternalAuthManager _externalAuthManager;

        /// <summary>
        /// Defines the _userRegistrationManager
        /// </summary>
        private readonly UserRegistrationManager _userRegistrationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenAuthController"/> class.
        /// </summary>
        /// <param name="logInManager">The logInManager<see cref="LogInManager"/></param>
        /// <param name="tenantCache">The tenantCache<see cref="ITenantCache"/></param>
        /// <param name="abpLoginResultTypeHelper">The abpLoginResultTypeHelper<see cref="AbpLoginResultTypeHelper"/></param>
        /// <param name="configuration">The configuration<see cref="TokenAuthConfiguration"/></param>
        /// <param name="externalAuthConfiguration">The externalAuthConfiguration<see cref="IExternalAuthConfiguration"/></param>
        /// <param name="externalAuthManager">The externalAuthManager<see cref="IExternalAuthManager"/></param>
        /// <param name="userRegistrationManager">The userRegistrationManager<see cref="UserRegistrationManager"/></param>
        public TokenAuthController(
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
        }

        /// <summary>
        /// The Authenticate
        /// </summary>
        /// <param name="model">The model<see cref="AuthenticateModel"/></param>
        /// <returns>The <see cref="Task{AuthenticateResultModel}"/></returns>
        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                GetTenancyNameOrNull()
            );

            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = loginResult.User.Id
            };
        }

        /// <summary>
        /// The GetExternalAuthenticationProviders
        /// </summary>
        /// <returns>The <see cref="List{ExternalLoginProviderInfoModel}"/></returns>
        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        /// <summary>
        /// The ExternalAuthenticate
        /// </summary>
        /// <param name="model">The model<see cref="ExternalAuthenticateModel"/></param>
        /// <returns>The <see cref="Task{ExternalAuthenticateResultModel}"/></returns>
        [HttpPost]
        public async Task<ExternalAuthenticateResultModel> ExternalAuthenticate([FromBody] ExternalAuthenticateModel model)
        {
            var externalUser = await GetExternalUserInfo(model);

            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {
                        var newUser = await RegisterExternalUserAsync(externalUser);
                        if (!newUser.IsActive)
                        {
                            return new ExternalAuthenticateResultModel
                            {
                                WaitingForActivation = true
                            };
                        }

                        // Try to login again with newly registered user!
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.AuthProvider, model.ProviderKey, model.AuthProvider), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                model.ProviderKey,
                                GetTenancyNameOrNull()
                            );
                        }

                        return new ExternalAuthenticateResultModel
                        {
                            AccessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity)),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds
                        };
                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            model.ProviderKey,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }

        /// <summary>
        /// The RegisterExternalUserAsync
        /// </summary>
        /// <param name="externalUser">The externalUser<see cref="ExternalAuthUserInfo"/></param>
        /// <returns>The <see cref="Task{User}"/></returns>
        private async Task<User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// The GetExternalUserInfo
        /// </summary>
        /// <param name="model">The model<see cref="ExternalAuthenticateModel"/></param>
        /// <returns>The <see cref="Task{ExternalAuthUserInfo}"/></returns>
        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        /// <summary>
        /// The GetTenancyNameOrNull
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        /// <summary>
        /// The GetLoginResultAsync
        /// </summary>
        /// <param name="usernameOrEmailAddress">The usernameOrEmailAddress<see cref="string"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        /// <param name="tenancyName">The tenancyName<see cref="string"/></param>
        /// <returns>The <see cref="Task{AbpLoginResult{Tenant, User}}"/></returns>
        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        /// <summary>
        /// The CreateAccessToken
        /// </summary>
        /// <param name="claims">The claims<see cref="IEnumerable{Claim}"/></param>
        /// <param name="expiration">The expiration<see cref="TimeSpan?"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// The CreateJwtClaims
        /// </summary>
        /// <param name="identity">The identity<see cref="ClaimsIdentity"/></param>
        /// <returns>The <see cref="List{Claim}"/></returns>
        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        /// <summary>
        /// The GetEncryptedAccessToken
        /// </summary>
        /// <param name="accessToken">The accessToken<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}
