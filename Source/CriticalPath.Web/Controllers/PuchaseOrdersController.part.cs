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
    public partial class PuchaseOrdersController 
    {
        partial void SetViewBags(PuchaseOrder puchaseOrder)
        {
            //TODO: Optimize query
            var queryCustomerId = DataContext.Companies.OfType<Customer>();
            int customerId = puchaseOrder == null ? 0 : puchaseOrder.CustomerId;
            ViewBag.CustomerId = new SelectList(queryCustomerId, "Id", "CompanyName", customerId);
        }

        //Purpose: To set default property values for newly created PuchaseOrder entity
        //partial void SetDefaults(PuchaseOrder puchaseOrder) { }
    }
}
