using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class ForgotViewModel
    {
        [EmailAddress]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "RequiredEmail")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Email")]
        public string Email { get; set; }
    }
}
