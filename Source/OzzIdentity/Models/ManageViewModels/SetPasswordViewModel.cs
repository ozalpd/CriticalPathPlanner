using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class SetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "MinLeght")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "ConfirmNewPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "ConfirmNewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
