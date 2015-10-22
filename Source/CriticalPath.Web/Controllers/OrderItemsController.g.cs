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
    public partial class OrderItemsController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetOrderItemQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Notes.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.PurchaseOrderId != null)
            {
                query = query.Where(x => x.PurchaseOrderId == qParams.PurchaseOrderId);
            }
            if (qParams.ProductId != null)
            {
                query = query.Where(x => x.ProductId == qParams.ProductId);
            }
            qParams.TotalCount = await query.CountAsync();
            SetPagerParameters(qParams);

            ViewBag.canUserEdit = await CanUserEdit();
            ViewBag.canUserCreate = await CanUserCreate();
            ViewBag.canUserDelete = await CanUserDelete();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<OrderItem>());   //there isn't any record, so no need to run a query
            }
        }
        
        protected virtual async Task<bool> CanUserCreate()
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

        protected virtual async Task<bool> CanUserEdit()
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
        
        protected virtual async Task<bool> CanUserDelete()
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
        public async Task<ActionResult> Details(int? id)  //GET: /OrderItems/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await FindAsyncOrderItem(id.Value);

            if (orderItem == null)
            {
                return HttpNotFound();
            }

            return View(orderItem);
        }


        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("OrderItems/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId)  //GET: /OrderItems/Create
        {
            var orderItem = new OrderItem();
            if (purchaseOrderId != null)
            {
                var purchaseOrder = await FindAsyncPurchaseOrder(purchaseOrderId.Value);
                if (purchaseOrder == null)
                    return HttpNotFound();
                orderItem.PurchaseOrder = purchaseOrder;
            }
            SetDefaults(orderItem);
            SetViewBags(null);
            return View(orderItem);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("OrderItems/Create/{purchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? purchaseOrderId, OrderItem orderItem)  //POST: /OrderItems/Create
        {
            DataContext.SetInsertDefaults(orderItem, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(orderItem);
 
                DataContext.OrderItems.Add(orderItem);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(orderItem);
                return RedirectToAction("Index");
            }

            SetViewBags(orderItem);
            return View(orderItem);
        }


        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /OrderItems/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await FindAsyncOrderItem(id.Value);

            if (orderItem == null)
            {
                return HttpNotFound();
            }

            SetViewBags(orderItem);
            return View(orderItem);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderItem orderItem)  //POST: /OrderItems/Edit/5
        {
            DataContext.SetInsertDefaults(orderItem, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(orderItem);
 
                DataContext.Entry(orderItem).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(orderItem);
                return RedirectToAction("Index");
            }

            SetViewBags(orderItem);
            return View(orderItem);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /OrderItems/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await FindAsyncOrderItem(id.Value);

            if (orderItem == null)
            {
                return HttpNotFound();
            }

            DataContext.OrderItems.Remove(orderItem);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(orderItem.Product.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? PurchaseOrderId { get; set; }
            public int? ProductId { get; set; }
        }

        //Partial methods
        partial void OnCreateSaving(OrderItem orderItem);
        partial void OnCreateSaved(OrderItem orderItem);
        partial void OnEditSaving(OrderItem orderItem);
        partial void OnEditSaved(OrderItem orderItem);
        partial void SetDefaults(OrderItem orderItem);
        partial void SetViewBags(OrderItem orderItem);
    }
}
