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
    public partial class CustomersController : BaseController 
    {
        partial void SetViewBags(Customer customer);
        partial void SetDefaults(Customer customer);

        
        [Authorize]
        public async Task<ActionResult> Index(string searchString, int pageNr = 1, int pageSize = 10)
        {
            var query = DataContext.GetCustomerQuery();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = from a in query
                        where
                            a.CompanyName.Contains(searchString) | 
                            a.CustomerCode.Contains(searchString) | 
                            a.Phone1.Contains(searchString) | 
                            a.Phone2.Contains(searchString) | 
                            a.Phone3.Contains(searchString) | 
                            a.Address1.Contains(searchString) | 
                            a.Address2.Contains(searchString) 
                        select a;
            }
            int totalCount = await query.CountAsync();
            int pageCount = totalCount > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 0;
            if (pageNr < 1) pageNr = 1;
            if (pageNr > pageCount) pageNr = pageCount;
            int skip = (pageNr - 1) * pageSize;

            ViewBag.pageNr = pageNr;
            ViewBag.totalCount = totalCount;
            ViewBag.pageSize = pageSize;
            ViewBag.pageCount = pageCount;

            ViewBag.canUserEdit = await CanUserEdit();
            ViewBag.canUserCreate = await CanUserCreate();
            ViewBag.canUserDelete = await CanUserDelete();

            if (totalCount > 0)
            {
                return View(await query.Skip(skip).Take(pageSize).ToListAsync());
            }
            else
            {
                return View(new List<Customer>());
            }
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /Customers/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /Customers/Create
        {
            var customer = new Customer();
            SetDefaults(customer);
            SetViewBags(null);
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)  //POST: /Customers/Create
        {
            DataContext.SetInsertDefaults(customer, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Companies.Add(customer);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(customer);
            return View(customer);
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
        public async Task<ActionResult> Edit(int? id)  //GET: /Customers/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }

            SetViewBags(customer);
            return View(customer);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)  //POST: /Customers/Edit/5
        {
            DataContext.SetInsertDefaults(customer, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Entry(customer).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(customer);
            return View(customer);
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
        public async Task<ActionResult> Delete(int? id)  //GET: /Customers/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await FindAsyncCustomer(id.Value);

            if (customer == null)
            {
                return HttpNotFound();
            }
            
            DataContext.Companies.Remove(customer);
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
