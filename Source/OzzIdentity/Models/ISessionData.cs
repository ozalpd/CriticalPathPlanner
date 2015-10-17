using System.Security.Principal;
using System.Threading.Tasks;

namespace OzzIdentity.Models
{
    /// <summary>
    /// Interface for passing session data to DbContext.
    /// Implement in to the Controllers or ASP.NET form pages 
    /// then simply pass the object into the DbContext.
    /// </summary>
    public interface ISessionData
    {
        Task<OzzUser> GetCurrentUserAsync();
        OzzUser GetCurrentUser();
        string GetUserIP();
        string UserID { get; }
        string SessionID { get; }
    }
}
