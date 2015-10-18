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
    public partial class ProductCategoriesController : BaseController 
    {
        partial void SetViewBags(ProductCategory productCategory);
        partial void SetDefaults(ProductCategory productCategory);

        
        
        public async Task<ActionResult> Index(string searchString, int pageNr = 1, int pageSize = 10)
        {
            var query = DataContext.GetProductCategoryQuery();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(searchString) | 
                            a.Code.Contains(searchString) 
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
                return View(new List<ProductCategory>());
            }
        }

        
        public async Task<ActionResult> Details(int? id)  //GET: /ProductCategories/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await FindAsyncProductCategory(id.Value);

            if (productCategory == null)
            {
                return HttpNotFound();
            }

            return View(productCategory);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /ProductCategories/Create
        {
            var productCategory = new ProductCategory();
            SetDefaults(productCategory);
            SetViewBags(null);
            return View(productCategory);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCategory productCategory)  //POST: /ProductCategories/Create
        {
            DataContext.SetInsertDefaults(productCategory, this);

            if (ModelState.IsValid)
            {
 
                DataContext.ProductCategories.Add(productCategory);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(productCategory);
            return View(productCategory);
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
        public async Task<ActionResult> Edit(int? id)  //GET: /ProductCategories/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await FindAsyncProductCategory(id.Value);

            if (productCategory == null)
            {
                return HttpNotFound();
            }

            SetViewBags(productCategory);
            return View(productCategory);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductCategory productCategory)  //POST: /ProductCategories/Edit/5
        {
            DataContext.SetInsertDefaults(productCategory, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Entry(productCategory).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(productCategory);
            return View(productCategory);
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
        public async Task<ActionResult> Delete(int? id)  //GET: /ProductCategories/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await FindAsyncProductCategory(id.Value);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            
            DataContext.ProductCategories.Remove(productCategory);
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
