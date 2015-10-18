using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CriticalPath.Data;

namespace CriticalPath.Web.Controllers
{
    public partial class ContactsController 
    {
        partial void SetViewBags(Contact contact)
        {
            //TODO: Optimize query
            var queryCompanyId = DataContext.Companies;
            int companyId = contact == null ? 0 : contact.CompanyId;
            ViewBag.CompanyId = new SelectList(queryCompanyId, "Id", "CompanyName", companyId);
        }

        //Purpose: To set default property values for newly created Contact entity
        //partial void SetDefaults(Contact contact) { }
    }
}
