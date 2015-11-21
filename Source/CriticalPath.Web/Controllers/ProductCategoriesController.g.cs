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
using CP.i8n;

namespace CriticalPath.Web.Controllers
{
    public partial class ProductCategoriesController : BaseController 
    {
        protected virtual async Task<List<ProductCategoryDTO>> GetProductCategoryDtoList(QueryParameters qParams)
        {
            var query = await GetProductCategoryQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<ProductCategory>();
            var result = new List<ProductCategoryDTO>();
            foreach (var item in list)
            {
                result.Add(new ProductCategoryDTO(item));
            }

            return result;
        }

        
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetProductCategoryQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<ProductCategory>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
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

        
        public async Task<ActionResult> GetProductCategoryList(QueryParameters qParams)
        {
            var result = await GetProductCategoryDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        public async Task<ActionResult> GetProductCategoryPagedList(QueryParameters qParams)
        {
            var items = await GetProductCategoryDtoList(qParams);
            var result = new PagedList<ProductCategoryDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
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

            await PutCanUserInViewBag();
            return View(productCategory);
        }

        
        public async Task<ActionResult> GetProductCategory(int? id)
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

            return Json(new ProductCategoryDTO(productCategory), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create()  //GET: /ProductCategories/Create
        {
            var productCategory = new ProductCategory();
            await SetProductCategoryDefaults(productCategory);
            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
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

            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
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

            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
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

            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
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
                sb.Append(productCategory.CategoryName);
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
                sb.Append(productCategory.CategoryName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                ParentCategoryId = parameters.ParentCategoryId;
            }
            public int? ParentCategoryId { get; set; }
        }

        public partial class PagedList<T> : QueryParameters
        {
            public PagedList() { }
            public PagedList(QueryParameters parameters) : base(parameters) { }
            public PagedList(QueryParameters parameters, IEnumerable<T> items) : this(parameters)
            {
                Items = items;
            }

            public IEnumerable<T> Items
            {
                set { _items = value; }
                get
                {
                    if (_items == null)
                    {
                        _items = new List<T>();
                    }
                    return _items;
                }
            }
            IEnumerable<T> _items;
        }
        partial void OnCreateSaving(ProductCategory productCategory);
        partial void OnCreateSaved(ProductCategory productCategory);
        partial void OnEditSaving(ProductCategory productCategory);
        partial void OnEditSaved(ProductCategory productCategory);
    }
}
