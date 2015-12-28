using CP.i8n;
using System.ComponentModel.DataAnnotations;

namespace CriticalPath.Data
{
    public partial class EmployeeDTO
    {
        partial void Initiliazing(Employee entity)
        {
            Constructing(entity);
        }

        protected virtual void Constructing(Employee employee)
        {
            if(employee.AspNetUser != null)
            {
                UserName = employee.AspNetUser.UserName;
                FirstName = employee.AspNetUser.FirstName;
                LastName = employee.AspNetUser.LastName;
                Email = employee.AspNetUser.Email;
                PhoneNumber = employee.AspNetUser.PhoneNumber;
            }
        }

        [StringLength(256, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Display(ResourceType = typeof(EntityStrings), Name = "UserName")]
        public string UserName { get; set; }

        [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(CommonStrings), Name = "FullName")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [EmailAddress]
        [Display(ResourceType = typeof(EntityStrings), Name = "Email")]
        public string Email { get; set; }

        [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [DataType(DataType.PhoneNumber)]
        [Display(ResourceType = typeof(EntityStrings), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}
