using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzzIdentity.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(256, ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(256, ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "LastName")]
        public string LastName { get; set; }

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

        public OzzUser ToUser()
        {
            var user = new OzzUser
            {
                UserName = this.Email,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            return user;
        }
    }
}
