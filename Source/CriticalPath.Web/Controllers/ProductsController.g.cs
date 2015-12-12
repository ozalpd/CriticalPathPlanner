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
                            a.ProductCode.Contains(qParams.SearchString) | 
                            a.Description.Contains(qParams.SearchString) | 
                            a.DiscontinueNotes.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == qParams.CategoryId);
            }
            if (qParams.SellingCurrencyId != null)
            {
                query = query.Where(x => x.SellingCurrencyId == qParams.SellingCurrencyId);
            }
            if (qParams.BuyingCurrencyId != null)
            {
                query = query.Where(x => x.BuyingCurrencyId == qParams.BuyingCurrencyId);
            }
            if (qParams.RoyaltyCurrencyId != null)
            {
                query = query.Where(x => x.RoyaltyCurrencyId == qParams.RoyaltyCurrencyId);
            }
            if (qParams.RetailCurrencyId != null)
            {
                query = query.Where(x => x.RetailCurrencyId == qParams.RetailCurrencyId);
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

        [Authorize]
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

        protected override async Task<bool> CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canSeeRestricted.Value;
        }
        bool? _canSeeRestricted;

        [Authorize]
        public async Task<ActionResult> GetProductList(QueryParameters qParams)
        {
            var result = await GetProductDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetProductPagedList(QueryParameters qParams)
        {
            var items = await GetProductDtoList(qParams);
            var result = new PagedList<ProductDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetProductsForAutoComplete(QueryParameters qParam)
        {
            var query = GetProductQuery()
                        .Where(x => x.ProductCode.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.ProductCode,
                           label = x.ProductCode //can be extended as x.Category.CategoryName + "/" + x.ProductCode,
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
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

        [Authorize]
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
                sb.Append(product.ProductCode);
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
                sb.Append(product.ProductCode);
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
                SellingCurrencyId = parameters.SellingCurrencyId;
                BuyingCurrencyId = parameters.BuyingCurrencyId;
                RoyaltyCurrencyId = parameters.RoyaltyCurrencyId;
                RetailCurrencyId = parameters.RetailCurrencyId;
            }
            public int? CategoryId { get; set; }
            public int? SellingCurrencyId { get; set; }
            public int? BuyingCurrencyId { get; set; }
            public int? RoyaltyCurrencyId { get; set; }
            public int? RetailCurrencyId { get; set; }
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
    }
}
