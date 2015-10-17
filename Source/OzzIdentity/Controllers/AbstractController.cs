using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using OzzIdentity.Models;

namespace OzzIdentity.Controllers
{
    public abstract class AbstractController : Controller, ISessionData
    {
        public AbstractController() { }
        public AbstractController(OzzUserManager userManager)
        {
            UserManager = userManager;
        }

        public OzzUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    return HttpContext.GetOwinContext().GetUserManager<OzzUserManager>();
                }
                return _userManager;
            }
            private set { _userManager = value; }
        }
        private OzzUserManager _userManager;


        public OzzRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    return HttpContext.GetOwinContext().GetUserManager<OzzRoleManager>();
                }
                return _roleManager;
            }
            private set { _roleManager = value; }
        }
        private OzzRoleManager _roleManager;


        public string UserID
        {
            get
            {
                if (User.Identity.IsAuthenticated && string.IsNullOrEmpty(_userId))
                {
                    _userId = User.Identity.GetUserId();
                }
                return _userId;
            }
        }
        string _userId = string.Empty;


        public OzzUser GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated && _currentUser == null)
            {
                _currentUser = UserManager.FindById(UserID);
            }
            return _currentUser;
        }

        public async Task<OzzUser> GetCurrentUserAsync()
        {
            if (User.Identity.IsAuthenticated && _currentUser == null)
            {
                _currentUser = await UserManager.FindByIdAsync(UserID);
            }
            return _currentUser;
        }
        OzzUser _currentUser;


        public string SessionID
        {
            get { return Session.SessionID; }
        }

        public virtual string GetUserIP()
        {
            if (string.IsNullOrEmpty(userIP)) userIP = Request.ServerVariables["HTTP_CLIENT_IP"];
            if (string.IsNullOrEmpty(userIP)) userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIP)) userIP = Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userIP)) userIP = Request.ServerVariables["REMOTE_HOST"];
            if (string.IsNullOrEmpty(userIP)) userIP = Request.UserHostAddress;

            return userIP;
        }
        string userIP = string.Empty;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
            base.Dispose(disposing);
        }




        // Used for XSRF protection when adding external logins
        protected const string XsrfKey = "XsrfId";

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}
