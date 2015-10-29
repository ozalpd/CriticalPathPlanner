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
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = GetPurchaseOrderQuery(qParams);
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<PurchaseOrder>());   //there isn't any record, so no need to run a query
            }
        }
        
        protected override async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;
        
        protected override async Task<bool> CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;

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
            var purchaseOrder = new PurchaseOrderCreateVM();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                purchaseOrder.CustomerId = customer.Id;
            }
            await SetPurchaseOrderDefaults(purchaseOrder);
            await SetProductSelectListAsync(purchaseOrder.Product);
            return View(purchaseOrder);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("PurchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, PurchaseOrderCreateVM purchaseOrder)  //POST: /PurchaseOrders/Create
        {
            DataContext.SetInsertDefaults(purchaseOrder, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(purchaseOrder);
                var entity = purchaseOrder.ToPurchaseOrder();
                DataContext.PurchaseOrders.Add(entity);
                await DataContext.SaveChangesAsync(this);
                OnCreateSaved(entity);
                return RedirectToAction("Details", new { id = purchaseOrder.Id });
            }

            await SetProductSelectListAsync(purchaseOrder.Product);
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

            await SetProductSelectListAsync(purchaseOrder.Product);
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

            await SetProductSelectListAsync(purchaseOrder.Product);
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

            int quantitySizeRatesCount = purchaseOrder.QuantitySizeRates.Count;
            if ((quantitySizeRatesCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(purchaseOrder.Title);
                sb.Append("</b>.<br/>");

                if (quantitySizeRatesCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, quantitySizeRatesCount, EntityStrings.QuantitySizeRates));
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
            public int? ProductId { get; set; }
        }


        //Partial methods
        partial void OnCreateSaving(PurchaseOrderCreateVM purchaseOrder);
        partial void OnCreateSaved(PurchaseOrder purchaseOrder);
        partial void OnEditSaving(PurchaseOrder purchaseOrder);
        partial void OnEditSaved(PurchaseOrder purchaseOrder);
    }
}
