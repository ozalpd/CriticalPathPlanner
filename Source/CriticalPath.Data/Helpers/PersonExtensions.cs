using OzzUtils;

namespace CriticalPath.Data.Helpers
{
    public static class PersonExtensions
    {
        public static string CreateUserName(this IPerson person)
        {
            return string.Format("{0}.{1}", person.FirstName, person.LastName)
                .Replace(" ", ".")
                .RemoveTurkishChars()
                .ToLowerInvariant();
        }
    }
}
