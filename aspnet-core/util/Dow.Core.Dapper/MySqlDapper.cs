using Abp.Application.Services.Dto;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Dow.Core.Dapper
{
    public class MySqlDapper : DapperBase
    {
        public MySqlDapper(string connectionString) : base(connectionString)
        {
        }
        protected override IDbConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "The connectionString of " + connectionString + " cannot be null.");
            IDbConnection conn = SqlClientFactory.Instance.CreateConnection();
            if (conn == null)
                throw new ArgumentNullException(nameof(IDbConnection), "Failed to get database connection object");
            conn.ConnectionString = connectionString;
            conn.Open();
            return conn;
        }

        public override async Task<IPagedResult<T>> QueryPageAsync<T>(string countSql, string dataSql, int pageindex, int pagesize, object param = null, int? commandTimeout = null)
        {
            if (pageindex < 1)
                throw new ArgumentException("The pageindex cannot be less then 1.");
            if (pagesize < 1)
                throw new ArgumentException("The pagesize cannot be less then 1.");
            var pars = new DynamicParameters();
            if (param != null)
                pars.AddDynamicParams(param);

            pars.AddDynamicParams(new
            {
                Skip = (pageindex - 1) * pagesize,
                Take = pagesize
            });
            dataSql += $" limit @Skip, @Take";
            using (var multi = await Conn.Value.QueryMultipleAsync($"{countSql}{(countSql.EndsWith(";") ? "" : ";")}{dataSql}", pars, Transaction, commandTimeout))
            {
                var count = (await multi.ReadAsync<int>()).FirstOrDefault();
                var data = (await multi.ReadAsync<T>()).AsList();
                return new PagedResultDto<T>
                {
                    TotalCount = count,
                    Items = data
                };
            }
        }

        public override async Task<IPagedResult<dynamic>> QueryPageAsync(string countSql, string dataSql, int pageindex, int pagesize, object param = null, int? commandTimeout = null)
        {
            if (pageindex < 1)
                throw new ArgumentException("The pageindex cannot be less then 1.");
            if (pagesize < 1)
                throw new ArgumentException("The pagesize cannot be less then 1.");
            var pars = new DynamicParameters();
            if (param != null)
                pars.AddDynamicParams(param);

            pars.AddDynamicParams(new
            {
                Skip = (pageindex - 1) * pagesize,
                Take = pagesize
            });
            dataSql += $" limit @Skip, @Take";
            using (var multi = await Conn.Value.QueryMultipleAsync($"{countSql}{(countSql.EndsWith(";") ? "" : ";")}{dataSql}", pars, Transaction, commandTimeout))
            {
                var count = (await multi.ReadAsync<int>()).FirstOrDefault();
                var data = (await multi.ReadAsync()).AsList();
                return new PagedResultDto<dynamic>
                {
                    TotalCount = count,
                    Items = data
                };
            }
        }

        public override IPagedResult<T> QueryPage<T>(string countSql, string dataSql, int pageindex, int pagesize, object param = null, int? commandTimeout = null)
        {
            if (pageindex < 1)
                throw new ArgumentException("The pageindex cannot be less then 1.");
            if (pagesize < 1)
                throw new ArgumentException("The pagesize cannot be less then 1.");
            var pars = new DynamicParameters();
            if (param != null)
                pars.AddDynamicParams(param);

            pars.AddDynamicParams(new
            {
                Skip = (pageindex - 1) * pagesize,
                Take = pagesize
            });
            dataSql += $" limit @Skip, @Take";
            using (var multi = Conn.Value.QueryMultiple($"{countSql}{(countSql.EndsWith(";") ? "" : ";")}{dataSql}", pars, Transaction, commandTimeout))
            {

                var count = multi.Read<int>().FirstOrDefault();
                var data = multi.Read<T>().AsList();
                return new PagedResultDto<T>
                {
                    TotalCount = count,
                    Items = data
                };
            }
        }
        public override IPagedResult<dynamic> QueryPage(string countSql, string dataSql, int pageindex, int pagesize, object param = null, int? commandTimeout = null)
        {
            if (pageindex < 1)
                throw new ArgumentException("The pageindex cannot be less then 1.");
            if (pagesize < 1)
                throw new ArgumentException("The pagesize cannot be less then 1.");
            var pars = new DynamicParameters();
            if (param != null)
                pars.AddDynamicParams(param);

            pars.AddDynamicParams(new
            {
                Skip = (pageindex - 1) * pagesize,
                Take = pagesize
            });
            dataSql += $" limit @Skip, @Take";
            using (var multi = Conn.Value.QueryMultiple($"{countSql}{(countSql.EndsWith(";") ? "" : ";")}{dataSql}", pars, Transaction, commandTimeout))
            {

                var count = multi.Read<int>().FirstOrDefault();
                var data = multi.Read().AsList();
                return new PagedResultDto<dynamic>
                {
                    TotalCount = count,
                    Items = data
                };
            }
        }
    }
}
