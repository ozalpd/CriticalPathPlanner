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
    public partial class PuchaseOrdersController 
    {
        partial void SetViewBags(PuchaseOrder puchaseOrder)
        {
            //TODO: Optimize query
            var queryCustomerId = DataContext.Companies.OfType<Customer>();
            int customerId = puchaseOrder == null ? 0 : puchaseOrder.CustomerId;
            ViewBag.CustomerId = new SelectList(queryCustomerId, "Id", "CompanyName", customerId);
        }


        //public new class QueryParameters : BaseController.QueryParameters
        //{

        //}

        partial void SetDefaults(PuchaseOrder puchaseOrder)
        {
            puchaseOrder.OrderDate = DateTime.Now.Date;
        }
    }
}
