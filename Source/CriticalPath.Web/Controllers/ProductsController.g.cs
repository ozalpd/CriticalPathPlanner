using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CriticalPath.Data;

namespace CriticalPath.Web.Controllers
{
    public partial class ProductsController : BaseController 
    {
        private async Task<Product> FindAsyncProduct(int id)
        {
            return await DataContext
                            .GetProductQuery()
                            .FirstOrDefaultAsync(x => x.Id == id);
        }
        partial void SetViewBags(Product product);
        partial void SetDefaults(Product product);

        
        
        public async Task<ActionResult> Index(string searchString, int pageNr = 1, int pageSize = 10)
        {
            var query = DataContext.GetProductQuery();
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
                return View(new List<Product>());
            }
        }

        
        public async Task<ActionResult> Details(int? id)  //GET: /Products/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /Products/Create
        {
            var product = new Product();
            SetDefaults(product);
            SetViewBags(null);
            return View(product);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)  //POST: /Products/Create
        {
            DataContext.SetInsertDefaults(product, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Products.Add(product);
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(product);
            return View(product);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Products/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            SetViewBags(product);
            return View(product);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)  //POST: /Products/Edit/5
        {
            DataContext.SetInsertDefaults(product, this);

            if (ModelState.IsValid)
            {
 
                DataContext.Entry(product).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                return RedirectToAction("Index");
            }

            SetViewBags(product);
            return View(product);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Products/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            
            DataContext.Products.Remove(product);
            await DataContext.SaveChangesAsync(this);

            return RedirectToAction("Index");
        }
        
        protected CriticalPathContext DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    _dataContext = new CriticalPathContext();
                }
                return _dataContext;
            }
        }
        private CriticalPathContext _dataContext;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
