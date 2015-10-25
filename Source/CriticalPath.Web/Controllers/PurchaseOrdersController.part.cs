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
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetPurchaseOrderQuery();
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

        protected override void SetPurchaseOrderDefaults(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.OrderDate = DateTime.Today;
        }

        partial void SetSelectLists(PurchaseOrder purchaseOrder)
        {
            var queryCustomerId = DataContext.GetCustomerDtoQuery();
            int customerId = purchaseOrder == null ? 0 : purchaseOrder.CustomerId;
            ViewBag.CustomerId = new SelectList(queryCustomerId, "Id", "CompanyName", customerId);
        }
    }
}
