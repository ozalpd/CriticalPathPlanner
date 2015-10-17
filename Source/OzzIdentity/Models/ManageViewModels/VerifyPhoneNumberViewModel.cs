using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Code")]
        public string Code { get; set; }

        [Phone]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
