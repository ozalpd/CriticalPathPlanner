using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class AddPhoneNumberViewModel
    {
        [Phone]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "PhoneNumber")]
        public string Number { get; set; }
    }
}
