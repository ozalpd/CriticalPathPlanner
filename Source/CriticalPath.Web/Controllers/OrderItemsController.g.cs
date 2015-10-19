using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class OrderItemsController : BaseController 
    {
        partial void SetViewBags(OrderItem orderItem);
        partial void SetDefaults(OrderItem orderItem);
        
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
        [Route("OrderItems/Create/{puchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? puchaseOrderId)  //GET: /OrderItems/Create
        {
            var orderItem = new OrderItem();
            if (puchaseOrderId != null)
            {
                var puchaseOrder = await FindAsyncPuchaseOrder(puchaseOrderId.Value);
                if (puchaseOrder == null)
                    return HttpNotFound();
                orderItem.PuchaseOrder = puchaseOrder;
            }
            SetDefaults(orderItem);
            SetViewBags(null);
            return View(orderItem);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("OrderItems/Create/{puchaseOrderId:int?}")]
        public async Task<ActionResult> Create(int? puchaseOrderId, OrderItem orderItem)  //POST: /OrderItems/Create
        {
            DataContext.SetInsertDefaults(orderItem, this);

            if (ModelState.IsValid)
            {
 
                DataContext.OrderItems.Add(orderItem);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Details", new { Id = orderItem.Id });
            }

            SetViewBags(orderItem);
            return View(orderItem);
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
 
                DataContext.Entry(orderItem).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(orderItem);
            return View(orderItem);
        }

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
            await DataContext.SaveChangesAsync(this);

            return RedirectToAction("Index");
        }
        
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
    }
}
