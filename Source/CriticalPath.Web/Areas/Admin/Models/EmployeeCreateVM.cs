using CriticalPath.Data;
using CriticalPath.Data.Helpers;
using OzzIdentity.Models;
using OzzIdentity.Resources;
using System.ComponentModel.DataAnnotations;

namespace CriticalPath.Web.Areas.Admin.Models
{
    public class EmployeeCreateVM : EmployeeDTO
    {
        public EmployeeCreateVM() { }
        public EmployeeCreateVM(Employee employee) : base(employee) { }

        public OzzUser ToUser()
        {
            var user = new OzzUser
            {
                UserName = this.CreateUserName(),
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };

            return user;
        }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MinLeght")]
        [Display(ResourceType = typeof(TitleStrings), Name = "NewPassword")]
        public string NewPassword { get; set; }
    }
}