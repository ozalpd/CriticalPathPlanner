using System.ComponentModel.DataAnnotations;

namespace OzzIdentity.Models
{
    public class UserViewModel
    {
        public UserViewModel() { }
        public UserViewModel(OzzUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
        }

        [Required(ErrorMessageResourceType = typeof(Resources.ErrorStrings), ErrorMessageResourceName = "RequiredEmail")]
        [Display(ResourceType = typeof(Resources.TitleStrings), Name = "UserName")]
        public string UserName { get; set; }

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


        public OzzUser ToUser()
        {
            var user = new OzzUser
            {
                UserName = this.UserName,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            return user;
        }
    }
}
