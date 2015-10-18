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
    public partial class PuchaseOrdersController : BaseController 
    {
        partial void SetViewBags(PuchaseOrder puchaseOrder);
        partial void SetDefaults(PuchaseOrder puchaseOrder);
        
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetPuchaseOrderQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) | 
                            a.Code.Contains(qParams.SearchString) | 
                            a.Description.Contains(qParams.SearchString) | 
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
                return View(new List<PuchaseOrder>());   //there isn't any record, so no need to run a query
            }
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /PuchaseOrders/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuchaseOrder puchaseOrder = await FindAsyncPuchaseOrder(id.Value);

            if (puchaseOrder == null)
            {
                return HttpNotFound();
            }

            return View(puchaseOrder);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("PuchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId)  //GET: /PuchaseOrders/Create
        {
            var puchaseOrder = new PuchaseOrder();
            if (customerId != null)
            {
                var customer = await FindAsyncCustomer(customerId.Value);
                if (customer == null)
                    return HttpNotFound();
                puchaseOrder.Customer = customer;
            }
            SetDefaults(puchaseOrder);
            SetViewBags(null);
            return View(puchaseOrder);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("PuchaseOrders/Create/{customerId:int?}")]
        public async Task<ActionResult> Create(int? customerId, PuchaseOrder puchaseOrder)  //POST: /PuchaseOrders/Create
        {
            DataContext.SetInsertDefaults(puchaseOrder, this);

            if (ModelState.IsValid)
            {
 
                DataContext.PuchaseOrders.Add(puchaseOrder);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(puchaseOrder);
            return View(puchaseOrder);
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
        public async Task<ActionResult> Edit(int? id)  //GET: /PuchaseOrders/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuchaseOrder puchaseOrder = await FindAsyncPuchaseOrder(id.Value);

            if (puchaseOrder == null)
            {
                return HttpNotFound();
            }

            SetViewBags(puchaseOrder);
            return View(puchaseOrder);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PuchaseOrder puchaseOrder)  //POST: /PuchaseOrders/Edit/5
        {
            DataContext.SetInsertDefaults(puchaseOrder, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Entry(puchaseOrder).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(puchaseOrder);
            return View(puchaseOrder);
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
        public async Task<ActionResult> Delete(int? id)  //GET: /PuchaseOrders/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuchaseOrder puchaseOrder = await FindAsyncPuchaseOrder(id.Value);

            if (puchaseOrder == null)
            {
                return HttpNotFound();
            }
            
            DataContext.PuchaseOrders.Remove(puchaseOrder);
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
