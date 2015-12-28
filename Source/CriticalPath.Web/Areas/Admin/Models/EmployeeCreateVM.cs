using CriticalPath.Data;
using OzzIdentity.Models;
using OzzUtils;

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
                UserName = string.Format("{0}.{1}", FirstName, LastName)
                            .RemoveTurkishChars()
                            .ToLowerInvariant()
                            .Replace(" ", "."),
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };

            return user;
        }
    }
}