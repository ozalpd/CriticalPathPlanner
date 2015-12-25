using OzzIdentity.Resources;
using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "RequiredEmail")]
        [Display(ResourceType = typeof(TitleStrings), Name = "UserName")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "RequiredPassword")]
        [Display(ResourceType = typeof(TitleStrings), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(TitleStrings), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
