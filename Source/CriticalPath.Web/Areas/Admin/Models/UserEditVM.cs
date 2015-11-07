using CriticalPath.Web.Models;
using OzzIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Areas.Admin.Models
{
    public partial class UserEditVM : UserViewModel
    {
        public UserEditVM() { }
        public UserEditVM(OzzUser user) : base(user)
        {
            Id = user.Id;
            UserName = user.UserName;
        }

        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Admin")]
        public bool IsUserAdmin { get; set; }

        [Display(Name = "Clerk")]
        public bool IsUserClerk { get; set; }

        [Display(Name = "Observer")]
        public bool IsUserObserver { get; set; }

        [Display(Name = "Supervisor")]
        public bool IsUserSupervisor { get; set; }

        [Display(Name = "Supplier")]
        public bool IsUserSupplier { get; set; }

        public void UpdateUser(OzzUser user)
        {
            if (user.Id.Equals(Id))
            {
                user.Email = Email;
                user.FirstName = FirstName;
                user.LastName = LastName;
            }
        }
        public bool IsUserInRole(string role)
        {
            switch (role)
            {
                case SecurityRoles.Admin:
                    return IsUserAdmin;

                case SecurityRoles.Clerk:
                    return IsUserClerk;

                case SecurityRoles.Observer:
                    return IsUserObserver;

                case SecurityRoles.Supervisor:
                    return IsUserSupervisor;

                case SecurityRoles.Supplier:
                    return IsUserSupplier;

                default:
                    return false;
            }
        }

        public void SetIsUserInRole(string role, bool isIn)
        {
            switch (role)
            {
                case SecurityRoles.Admin:
                    IsUserAdmin = isIn;
                    break;

                case SecurityRoles.Clerk:
                    IsUserClerk = isIn;
                    break;

                case SecurityRoles.Observer:
                    IsUserObserver = isIn;
                    break;

                case SecurityRoles.Supervisor:
                    IsUserSupervisor = isIn;
                    break;

                case SecurityRoles.Supplier:
                    IsUserSupplier = isIn;
                    break;

                default:
                    break;
            }
        }
    }
}
