using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "RequiredPassword")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "CurrentPassword")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "MinLeght")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "ConfirmNewPassword")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "ConfirmNewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
