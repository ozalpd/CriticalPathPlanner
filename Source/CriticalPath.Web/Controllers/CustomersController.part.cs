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
    public partial class CustomersController 
    {
        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserAddPurchaseOrder = await CanUserAddPurchaseOrder();
            ViewBag.canUserAddContact = await CanUserAddContact();

            await base.PutCanUserInViewBag();
        }

        partial void OnCreateSaving(Customer customer)
        {
            customer.IsActive = true;
        }

        protected override Task SetCustomerDefaults(Customer customer)
        {
            customer.IsActive = true;
            return base.SetCustomerDefaults(customer);
        }
    }
}
