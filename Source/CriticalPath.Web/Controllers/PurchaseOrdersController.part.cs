using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using CriticalPath.Data.Resources;

namespace CriticalPath.Web.Controllers
{
    public partial class PurchaseOrdersController 
    {
        partial void SetViewBags(PurchaseOrder purchaseOrder)
        {
            var queryCustomerId = DataContext.GetCustomerDtoQuery();
            int customerId = purchaseOrder == null ? 0 : purchaseOrder.CustomerId;
            ViewBag.CustomerId = new SelectList(queryCustomerId, "Id", "CompanyName", customerId);
        }


        partial void SetDefaults(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
        }
    }
}
