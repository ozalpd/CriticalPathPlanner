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
            await PutCanUserInViewBag();
            var query = await GetProductCategoryQuery(qParams);
            var result = new PagedList<ProductCategory>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

        
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

        
        public async Task<JsonResult> GetProductCategoriesForAutoComplete(QueryParameters qParam)
        {
            var query = GetProductCategoryQuery()
                        .Where(x => x.CategoryName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.CategoryName,
                           label = x.CategoryName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        
        public async Task<ActionResult> Details(int? id, bool? modal)
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
            if (modal ?? false)
            {
                return PartialView("_Details", productCategory);
            }
            return View(productCategory);
        }

        
        public async Task<ActionResult> GetProductCategory(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            ProductCategory productCategory = await FindAsyncProductCategory(id.Value);

            if (productCategory == null)
            {
                return NotFoundTextResult();
            }

            return Json(new ProductCategoryDTO(productCategory), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create(bool? modal)
        {
            var productCategory = new ProductCategory();
            await SetProductCategoryDefaults(productCategory);
            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", productCategory);
            }
            return View(productCategory);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCategory productCategory, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(productCategory);
 
                DataContext.ProductCategories.Add(productCategory);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(productCategory);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", productCategory);
            }
            return View(productCategory);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
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
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", productCategory);
            }
            return View(productCategory);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductCategory productCategory, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(productCategory);
 
                DataContext.Entry(productCategory).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(productCategory);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            await SetParentCategorySelectList(productCategory?.ParentCategoryId ?? 0);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", productCategory);
            }
            return View(productCategory);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /ProductCategories
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            ProductCategory productCategory = await FindAsyncProductCategory(id.Value);

            if (productCategory == null)
            {
                return NotFoundTextResult();
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

                return StatusCodeTextResult(sb, HttpStatusCode.BadRequest);
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

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override bool CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserCreate.Value;
        }
        protected override async Task<bool> CanUserCreateAsync()
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

        protected override bool CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserEdit.Value;
        }
        protected override async Task<bool> CanUserEditAsync()
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
        
        protected override bool CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor());
            }
            return _canUserDelete.Value;
        }
        protected override async Task<bool> CanUserDeleteAsync()
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

        
        protected override bool CanUserSeeRestricted() { return true; }
        protected override Task<bool> CanUserSeeRestrictedAsync() { return Task.FromResult(true); }


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
