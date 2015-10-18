using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class ContactsController 
    {
        partial void SetViewBags(Contact contact)
        {
            //TODO: Optimize query
            var queryCompanyId = DataContext.Companies;
            //int companyId = contact == null ? 0 : contact.CompanyId;
            //ViewBag.CompanyId = new SelectList(queryCompanyId, "Id", "CompanyName", companyId);
        }


        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

        //Purpose: To set default property values for newly created Contact entity
        //partial void SetDefaults(Contact contact) { }
    }
}
