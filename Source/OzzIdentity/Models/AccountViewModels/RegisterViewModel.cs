using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzzIdentity.Models
{
    public class RegisterViewModel : UserViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "MinLeght")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
