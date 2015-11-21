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
using CP.i8n;

namespace CriticalPath.Web.Controllers
{
    public partial class PurchaseOrdersController
    {
        protected virtual async Task<IQueryable<PurchaseOrder>> GetPurchaseOrderQuery(QueryParameters qParams)
        {
            var query = GetPurchaseOrderQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                           a.Product.ProductCode.Contains(qParams.SearchString) |
                           a.PoNr.Contains(qParams.SearchString) |
                           a.Description.Contains(qParams.SearchString) |
                           a.Customer.CompanyName.Contains(qParams.SearchString) |
                           a.Notes.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == qParams.CustomerId);
            }

            qParams.TotalCount = await query.CountAsync();

            return query;
        }

        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserApprove = await CanUserApprove();
            ViewBag.canUserCancelPO = await CanUserCancelPO();
            ViewBag.canUserSeeCustomer = await CanUserSeeCustomer();

            await base.PutCanUserInViewBag();
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
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

            await PutCanUserInViewBag();
            return View(new PurchaseOrderVM(purchaseOrder));
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(int? id)  //GET: /PurchaseOrders/Approve/5
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
                //TODO: Get 42 days from an AppSetting
                purchaseOrder.DueDate = DateTime.Today.AddDays(42);
            }
            var purchaseOrderVM = new PurchaseOrderVM(purchaseOrder);
            await SetCustomerSelectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(PurchaseOrderVM vm)
        {
            var purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            if (purchaseOrder == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            PutVmToPO(vm, purchaseOrder, true);

            if (vm.IsApproved)
            {
                await ApproveSaveAsync(purchaseOrder);
                return RedirectToAction("Create", "Processes", new { purchaseOrderId = purchaseOrder.Id });
            }
            var poVM = new PurchaseOrderVM(purchaseOrder);
            await SetCustomerSelectListAsync(vm);
            return View(poVM);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> CancelPO(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrder = await FindAsyncPurchaseOrder(id.Value);

            if (purchaseOrder == null)
                return HttpNotFound();

            if (purchaseOrder.Cancelled)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var purchaseOrderVM = new PurchaseOrderCancelVM(purchaseOrder);
            return View(purchaseOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> CancelPO(PurchaseOrderCancelVM vm)
        {
            var purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            if (purchaseOrder == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(vm.CancelNotes) && vm.Cancelled)
            {
                purchaseOrder.CancelNotes = vm.CancelNotes;
                CancelCancellation(purchaseOrder);
                foreach (var item in purchaseOrder.Processes)
                {
                    item.CancelNotes = vm.CancelNotes;
                    CancelCancellation(item);
                }
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }
            var poVM = new PurchaseOrderCancelVM(purchaseOrder);
            return View(poVM);
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

            var purchaseOrderVM = new PurchaseOrderVM(purchaseOrder);
            await SetProductSelectListAsync(purchaseOrderVM.Product);
            await SetSizingStandardSelectListAsync(purchaseOrderVM);
            await SetCustomerSelectListAsync(purchaseOrderVM);
            return View(purchaseOrderVM);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrderVM vm)  //POST: /PurchaseOrders/Edit/5
        {
            PurchaseOrder purchaseOrder = await FindAsyncPurchaseOrder(vm.Id);
            PutVmToPO(vm, purchaseOrder, false);
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

            int sizeRatesCount = purchaseOrder.SizeRatios.Count;
            if ((sizeRatesCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(purchaseOrder.Product.ProductCode);
                sb.Append("</b>.<br/>");

                if (sizeRatesCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, sizeRatesCount, EntityStrings.SizeRatios));
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
                sb.Append(purchaseOrder.Product.ProductCode);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private static void PutVmToPO(PurchaseOrderVM vm, PurchaseOrder purchaseOrder, bool isApproving)
        {
            purchaseOrder.CustomerPoNr = vm.CustomerPoNr;
            purchaseOrder.Description = vm.Description;
            purchaseOrder.Notes = vm.Notes;

            if (isApproving)
            {
                purchaseOrder.DueDate = vm.DueDate;
            }
            if (!purchaseOrder.IsApproved || isApproving)
            {
                purchaseOrder.CustomerId = vm.CustomerId > 0 ? vm.CustomerId : purchaseOrder.CustomerId;
                purchaseOrder.Quantity = vm.Quantity > 0 ? vm.Quantity : purchaseOrder.Quantity;
                purchaseOrder.UnitPrice = vm.UnitPrice;
            }
        }

        protected override Task SetPurchaseOrderDefaults(PurchaseOrderDTO purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
            return Task.FromResult(default(object));
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public bool? IsCancelled { get; set; }
            public bool? IsApproved { get; set; }
        }
    }
}
