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

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(int? id)  //GET: /PurchaseOrders/Edit/5
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
                return HttpNotFound();

            if(purchaseOrder.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(PurchaseOrderDTO vm)
        {
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            if (vm.IsApproved)
            {
                purchaseOrder.IsApproved = true;
                purchaseOrder.ApproveDate = DateTime.Today;
                purchaseOrder.ApprovedUserId = UserID;
                purchaseOrder.ApprovedUserIp = GetUserIP();
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }

            return View(purchaseOrder);
        }


        partial void SetDefaults(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
        }
    }
}
