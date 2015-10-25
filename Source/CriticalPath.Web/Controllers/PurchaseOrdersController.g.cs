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
    public partial class PurchaseOrdersController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /PurchaseOrders/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }

            return View(purchaseOrder);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId)  //GET: /PurchaseOrders/Create
        {
            var purchaseOrder = new PurchaseOrder();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                purchaseOrder.Customer = customer;
            }
            SetPurchaseOrderDefaults(purchaseOrder);
            SetSelectLists(null);
            return View(purchaseOrder);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, PurchaseOrder purchaseOrder)  //POST: /PurchaseOrders/Create
        {
            DataContext.SetInsertDefaults(purchaseOrder, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(purchaseOrder);
 
                DataContext.PurchaseOrders.Add(purchaseOrder);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(purchaseOrder);
                return RedirectToAction("Create", "OrderItems", new { purchaseOrderId = purchaseOrder.Id });
            }

            SetSelectLists(purchaseOrder);
            return View(purchaseOrder);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /PurchaseOrders/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(purchaseOrder);
            return View(purchaseOrder);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrder purchaseOrder)  //POST: /PurchaseOrders/Edit/5
        {
            DataContext.SetInsertDefaults(purchaseOrder, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(purchaseOrder);
 
                DataContext.Entry(purchaseOrder).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(purchaseOrder);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }

            SetSelectLists(purchaseOrder);
            return View(purchaseOrder);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /PurchaseOrders/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }

            int orderItemsCount = purchaseOrder.OrderItems.Count;
            if ((orderItemsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(purchaseOrder.Title);
                sb.Append("</b>.<br/>");

                if (orderItemsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, orderItemsCount, EntityStrings.OrderItems));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.PurchaseOrders.Remove(purchaseOrder);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(purchaseOrder.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? CustomerId { get; set; }
        }


        //Partial methods
        partial void OnCreateSaving(PurchaseOrder purchaseOrder);
        partial void OnCreateSaved(PurchaseOrder purchaseOrder);
        partial void OnEditSaving(PurchaseOrder purchaseOrder);
        partial void OnEditSaved(PurchaseOrder purchaseOrder);
        partial void SetSelectLists(PurchaseOrder purchaseOrder);
    }
}
