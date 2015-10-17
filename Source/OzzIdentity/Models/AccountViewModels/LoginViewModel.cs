using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "RequiredEmail")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "RequiredPassword")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
