using CriticalPath.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public class AppSettingsController : Controller //AbstractController
    {
        // GET: Admin/AppSettings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectTheme(string theme)
        {
            if (!string.IsNullOrEmpty(theme))
                AppSettings.SelectedTheme = theme;

            var vm = new SelectThemeVM();
            vm.Theme = AppSettings.SelectedTheme;

            return View(vm);
        }
    }
}