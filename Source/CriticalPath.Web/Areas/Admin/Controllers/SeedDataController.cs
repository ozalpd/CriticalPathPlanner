﻿using CriticalPath.Web.Controllers;
using CriticalPath.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SecurityRoles.Admin)]
    public class SeedDataController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}