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
    public partial class ProductsController : BaseController 
    {
        protected virtual async Task<IQueryable<Product>> GetProductQuery(QueryParameters qParams)
        {
            var query = GetProductQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) | 
                            a.Code.Contains(qParams.SearchString) | 
                            a.ImageUrl.Contains(qParams.SearchString) | 
                            a.DiscontinueNotes.Contains(qParams.SearchString) | 
                            a.DiscontinuedUserIp.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == qParams.CategoryId);
            }
            if (qParams.SizingStandardId != null)
            {
                query = query.Where(x => x.SizingStandardId == qParams.SizingStandardId);
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<ProductDTO>> GetProductDtoList(QueryParameters qParams)
        {
            var query = await GetProductQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<Product>();
            var result = new List<ProductDTO>();
            foreach (var item in list)
            {
                result.Add(new ProductDTO(item));
            }

            return result;
        }

        
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetProductQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<Product>(qParams);
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

        
        public async Task<ActionResult> GetProductList(QueryParameters qParams)
        {
            var result = await GetProductDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        public async Task<ActionResult> GetProductPagedList(QueryParameters qParams)
        {
            var result = new PagedList<ProductDTO>(qParams);
            result.Items = await GetProductDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
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

            await PutCanUserInViewBag();
            return View(product);
        }

        
        public async Task<ActionResult> GetProduct(int? id)
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

            return Json(new ProductDTO(product), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Create()  //GET: /Products/Create
        {
            var product = new Product();
            await SetProductDefaults(product);
            await SetProductCategorySelectListAsync(product);
            await SetSizingStandardSelectListAsync(product.SizingStandardId);
            
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
            await SetSizingStandardSelectListAsync(product.SizingStandardId);
            
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

            await SetProductCategorySelectListAsync(product);
            await SetSizingStandardSelectListAsync(product.SizingStandardId);
            
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
                OnEditSaving(product);
 
                DataContext.Entry(product).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(product);
                return RedirectToAction("Index");
            }

            await SetProductCategorySelectListAsync(product);
            await SetSizingStandardSelectListAsync(product.SizingStandardId);
            
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

            int purchaseOrdersCount = product.PurchaseOrders.Count;
            int suppliersCount = product.Suppliers.Count;
            if ((purchaseOrdersCount + suppliersCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(product.Title);
                sb.Append("</b>.<br/>");

                if (purchaseOrdersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, purchaseOrdersCount, EntityStrings.PurchaseOrders));
                    sb.Append("<br/>");
                }

                if (suppliersCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, suppliersCount, EntityStrings.Suppliers));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Products.Remove(product);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(product.Title);
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
                CategoryId = parameters.CategoryId;
                SizingStandardId = parameters.SizingStandardId;
            }
            public int? CategoryId { get; set; }
            public int? SizingStandardId { get; set; }
        }

        public partial class PagedList<T> : QueryParameters
        {
            public PagedList() { }
            public PagedList(QueryParameters parameters) : base(parameters) { }

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
        partial void OnCreateSaving(Product product);
        partial void OnCreateSaved(Product product);
        partial void OnEditSaving(Product product);
        partial void OnEditSaved(Product product);
    }
}
