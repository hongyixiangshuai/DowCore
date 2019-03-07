using System.ComponentModel.DataAnnotations;

namespace Dow.Core.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}