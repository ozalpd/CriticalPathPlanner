using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class ResetPasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "RequiredEmail")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "MinLeght")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
