using CP.i8n;
using CriticalPath.Data;
using OzzIdentity.Resources;
using System.ComponentModel.DataAnnotations;

namespace CriticalPath.Web.Areas.Admin.Models
{
    public class EmployeePasswordResetVM : EmployeeDTO
    {
        public EmployeePasswordResetVM() { }
        public EmployeePasswordResetVM(Employee entity) : base(entity)
        {
            AspNetUserId = entity.AspNetUserId;
        }

        public string AspNetUserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(CP.i8n.ErrorStrings), ErrorMessageResourceName = "Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(CP.i8n.ErrorStrings), ErrorMessageResourceName = "MinLeght")]
        [Display(ResourceType = typeof(TitleStrings), Name = "NewPassword")]
        public string NewPassword { get; set; }
    }
}