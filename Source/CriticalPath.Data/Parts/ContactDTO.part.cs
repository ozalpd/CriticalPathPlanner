namespace CriticalPath.Data
{
    public partial class ContactDTO
    {
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
