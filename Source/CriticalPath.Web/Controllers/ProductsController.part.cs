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
    public partial class ProductsController
    {

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create()  //GET: /Products/Create
        {
            var product = new Product();
            SetProductDefaults(product);
            await SetProductCategorySelectListAsync(product);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)  //POST: /Products/Create
        {
            DataContext.SetInsertDefaults(product, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(product);

                DataContext.Products.Add(product);
                await DataContext.SaveChangesAsync(this);

                OnCreateSaved(product);
                return RedirectToAction("Index");
            }

            await SetProductCategorySelectListAsync(product);
            return View(product);
        }


        public async Task<ActionResult> Products(int categoryId)
        {
            var query = from p in DataContext.GetProductDtoQuery()
                        where p.CategoryId == categoryId
                        select p;

            var result = await query.ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        partial void SetSelectLists(Product product)
        {
            var queryCategoryId = DataContext
                                    .GetProductCategoryQuery()
                                    .Where(c => c.SubCategories.Count == 0);
            int categoryId = product == null ? 0 : product.CategoryId;
            ViewBag.CategoryId = new SelectList(queryCategoryId, "Id", "Title", categoryId);

            ViewBag.ParentCategoryId = new SelectList(queryCategoryId, "Id", "Title", categoryId);
        }
    }
}
