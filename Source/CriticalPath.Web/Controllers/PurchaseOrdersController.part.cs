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
        protected virtual IQueryable<PurchaseOrder> GetPurchaseOrderQuery(QueryParameters qParams)
        {
            var query = GetPurchaseOrderQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                           a.Title.Contains(qParams.SearchString) |
                           a.Code.Contains(qParams.SearchString) |
                           a.Description.Contains(qParams.SearchString) |
                           a.Customer.CompanyName.Contains(qParams.SearchString) |
                           a.Notes.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == qParams.CustomerId);
            }

            return query;
        }

        //TODO:Put Sizes; XS S M L XL XXL 3XL 4XL TOTAL
        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserApprove = await CanUserApprove();
            await base.PutCanUserInViewBag();
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(int? id)  //GET: /PurchaseOrders/Edit/5
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
                return HttpNotFound();

            if(purchaseOrder.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (purchaseOrder.DueDate == null)
            {
                //TODO:get 42 days from an AppSetting
                purchaseOrder.DueDate = DateTime.Today.AddDays(42);
            }

            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(PurchaseOrderDTO vm)
        {
            var purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            if (vm.IsApproved && purchaseOrder != null)
            {
                await ApproveSaveAsync(purchaseOrder);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }

            return View(purchaseOrder);
        }

        protected override Task SetPurchaseOrderDefaults(PurchaseOrderCreateVM purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
            return Task.FromResult(default(object));
        }

        //partial void SetSelectLists(PurchaseOrder purchaseOrder)
        //{
        //    var queryCustomerId = DataContext.GetCustomerDtoQuery();
        //    int customerId = purchaseOrder == null ? 0 : purchaseOrder.CustomerId;
        //    ViewBag.CustomerId = new SelectList(queryCustomerId, "Id", "CompanyName", customerId);
        //}
    }
}
