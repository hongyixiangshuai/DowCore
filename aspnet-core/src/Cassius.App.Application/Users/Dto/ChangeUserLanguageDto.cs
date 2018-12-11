using System.ComponentModel.DataAnnotations;

namespace Cassius.App.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}