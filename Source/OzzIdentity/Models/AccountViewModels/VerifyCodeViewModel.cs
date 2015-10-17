using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "RememberBrowser")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}
