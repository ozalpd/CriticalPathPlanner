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
                           a.Product.Title.Contains(qParams.SearchString) |
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

        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserApprove = await CanUserApprove();
            ViewBag.canUserCancelPO = await CanUserCancelPO();

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
                purchaseOrder.DueDate = vm.DueDate;
                await ApproveSaveAsync(purchaseOrder);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }

            return View(purchaseOrder);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId)
        {
            var purchaseOrderVM = new PurchaseOrderCreateVM();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                purchaseOrderVM.CustomerId = customer.Id;
            }
            await SetPurchaseOrderDefaults(purchaseOrderVM);
            await SetProductSelectListAsync(purchaseOrderVM.Product);
            await SetSizingStandardSelectListAsync(purchaseOrderVM);
            await SetCustomerSelectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, PurchaseOrderCreateVM purchaseOrderVM)
        {
            DataContext.SetInsertDefaults(purchaseOrderVM, this);

            if (ModelState.IsValid)
            {
                var entity = purchaseOrderVM.ToPurchaseOrder();
                DataContext.PurchaseOrders.Add(entity);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { id = entity.Id });
            }

            await SetProductSelectListAsync(purchaseOrderVM.Product);
            await SetSizingStandardSelectListAsync(purchaseOrderVM);
            await SetCustomerSelectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
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

            var purchaseOrderVM = new PurchaseOrderEditVM(purchaseOrder);
            await SetProductSelectListAsync(purchaseOrderVM.Product);
            await SetSizingStandardSelectListAsync(purchaseOrderVM);
            await SetCustomerSelectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrderEditVM vm)  //POST: /PurchaseOrders/Edit/5
        {
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            purchaseOrder.Notes = vm.Notes;
            purchaseOrder.Code = vm.Code;
            purchaseOrder.Description = vm.Description;
            if (!purchaseOrder.IsApproved)
            {
                purchaseOrder.CustomerId = vm.CustomerId > 0 ? vm.CustomerId : purchaseOrder.CustomerId;
                purchaseOrder.Quantity = vm.Quantity > 0 ? vm.Quantity : purchaseOrder.Quantity;
                purchaseOrder.UnitPrice = vm.UnitPrice;
            }
            await DataContext.SaveChangesAsync(this);
            return RedirectToAction("Details", new { id = vm.Id });
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

            if (purchaseOrder.IsApproved)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            int sizeRatesCount = purchaseOrder.SizeRates.Count;
            if ((sizeRatesCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(purchaseOrder.Product.Title);
                sb.Append("</b>.<br/>");

                if (sizeRatesCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, sizeRatesCount, EntityStrings.SizeRates));
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
                sb.Append(purchaseOrder.Product.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override Task SetPurchaseOrderDefaults(PurchaseOrderDTO purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
            purchaseOrder.IsActive = true;
            return Task.FromResult(default(object));
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public bool? IsActive { get; set; }
            public bool? IsApproved { get; set; }
        }
    }
}
