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
    public partial class ProductCategoriesController : BaseController 
    {
        
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetProductCategoryQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) | 
                            a.Code.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.ParentCategoryId != null)
            {
                query = query.Where(x => x.ParentCategoryId == qParams.ParentCategoryId);
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
                return View(new List<ProductCategory>());   //there isn't any record, so no need to run a query
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

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /ProductCategories/Create
        {
            var productCategory = new ProductCategory();
            SetProductCategoryDefaults(productCategory);
            SetSelectLists(null);
            return View(productCategory);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCategory productCategory)  //POST: /ProductCategories/Create
        {
            DataContext.SetInsertDefaults(productCategory, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(productCategory);
 
                DataContext.ProductCategories.Add(productCategory);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(productCategory);
                return RedirectToAction("Index");
            }

            SetSelectLists(productCategory);
            return View(productCategory);
        }

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

            SetSelectLists(productCategory);
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
                OnEditSaving(productCategory);
 
                DataContext.Entry(productCategory).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(productCategory);
                return RedirectToAction("Index");
            }

            SetSelectLists(productCategory);
            return View(productCategory);
        }


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

            int subCategoriesCount = productCategory.SubCategories.Count;
            int productsCount = productCategory.Products.Count;
            if ((subCategoriesCount + productsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(productCategory.Title);
                sb.Append("</b>.<br/>");

                if (subCategoriesCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, subCategoriesCount, EntityStrings.SubCategories));
                    sb.Append("<br/>");
                }

                if (productsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, productsCount, EntityStrings.Products));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.ProductCategories.Remove(productCategory);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(productCategory.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? ParentCategoryId { get; set; }
        }


        //Partial methods
        partial void OnCreateSaving(ProductCategory productCategory);
        partial void OnCreateSaved(ProductCategory productCategory);
        partial void OnEditSaving(ProductCategory productCategory);
        partial void OnEditSaved(ProductCategory productCategory);
        partial void SetSelectLists(ProductCategory productCategory);
    }
}
