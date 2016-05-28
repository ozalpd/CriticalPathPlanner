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
                            a.Description.Contains(qParams.SearchString) 
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
            if (qParams.SellingCurrency2Id != null)
            {
                query = query.Where(x => x.SellingCurrency2Id == qParams.SellingCurrency2Id);
            }
            if (qParams.BuyingCurrencyId != null)
            {
                query = query.Where(x => x.BuyingCurrencyId == qParams.BuyingCurrencyId);
            }
            if (qParams.BuyingCurrency2Id != null)
            {
                query = query.Where(x => x.BuyingCurrency2Id == qParams.BuyingCurrency2Id);
            }
            if (qParams.LicensorCurrencyId != null)
            {
                query = query.Where(x => x.LicensorCurrencyId == qParams.LicensorCurrencyId);
            }
            if (qParams.RoyaltyCurrencyId != null)
            {
                query = query.Where(x => x.RoyaltyCurrencyId == qParams.RoyaltyCurrencyId);
            }
            if (qParams.RetailCurrencyId != null)
            {
                query = query.Where(x => x.RetailCurrencyId == qParams.RetailCurrencyId);
            }
            if (qParams.Licensed != null)
            {
                query = query.Where(x => x.Licensed == qParams.Licensed.Value);
            }
            if (qParams.Discontinued != null)
            {
                query = query.Where(x => x.Discontinued == qParams.Discontinued.Value);
            }
            if (qParams.DiscontinueDateMin != null)
            {
                query = query.Where(x => x.DiscontinueDate >= qParams.DiscontinueDateMin.Value);
            }
            if (qParams.DiscontinueDateMax != null)
            {
                var maxDate = qParams.DiscontinueDateMax.Value.AddDays(1);
                query = query.Where(x => x.DiscontinueDate < maxDate);
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
            await PutCanUserInViewBag();
            var query = await GetProductQuery(qParams);
            var result = new PagedList<Product>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

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
                           label = x.ProductCode
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id, bool? modal)
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
            if (modal ?? false)
            {
                return PartialView("_Details", product);
            }
            return View(product);
        }

        [Authorize]
        public async Task<ActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return NotFoundTextResult();
            }

            return Json(new ProductDTO(product), JsonRequestBehavior.AllowGet);
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

        protected override bool CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canSeeRestricted.Value;
        }
        protected override async Task<bool> CanUserSeeRestrictedAsync()
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



        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                CategoryId = parameters.CategoryId;
                SellingCurrencyId = parameters.SellingCurrencyId;
                SellingCurrency2Id = parameters.SellingCurrency2Id;
                BuyingCurrencyId = parameters.BuyingCurrencyId;
                BuyingCurrency2Id = parameters.BuyingCurrency2Id;
                LicensorCurrencyId = parameters.LicensorCurrencyId;
                RoyaltyCurrencyId = parameters.RoyaltyCurrencyId;
                RetailCurrencyId = parameters.RetailCurrencyId;
            }
            public int? CategoryId { get; set; }
            public int? SellingCurrencyId { get; set; }
            public int? SellingCurrency2Id { get; set; }
            public int? BuyingCurrencyId { get; set; }
            public int? BuyingCurrency2Id { get; set; }
            public int? LicensorCurrencyId { get; set; }
            public int? RoyaltyCurrencyId { get; set; }
            public int? RetailCurrencyId { get; set; }
            public bool? Licensed { get; set; }
            public bool? Discontinued { get; set; }
            public DateTime? DiscontinueDateMin { get; set; }
            public DateTime? DiscontinueDateMax { get; set; }
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
