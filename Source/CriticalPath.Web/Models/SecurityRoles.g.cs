namespace CriticalPath.Web.Models
{
    public static partial class SecurityRoles
    {
        public const string Admin = "admin";
        public const string Clerk = "clerk";
        public const string Observer = "observer";
        public const string Supervisor = "supervisor";
        public const string Supplier = "supplier";

        public static string[] ApplicationRoles
        {
            get
            {
                return new string[] {
                    Admin,
                    Clerk,
                    Observer,
                    Supervisor,
                    Supplier,
                };
            }
        }
    }
}