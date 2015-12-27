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
using OzzUtils.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class SuppliersController
    {
        protected virtual async Task<IQueryable<Supplier>> GetSupplierQuery(QueryParameters qParams)
        {
            var query = GetSupplierQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CompanyName.Contains(qParams.SearchString) |
                            a.SupplierCode.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.CountryId != null)
            {
                query = query.Where(x => x.CountryId == qParams.CountryId);
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
                query = query.Where(x => x.DiscontinueDate <= qParams.DiscontinueDateMax.Value);
            }
            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        [Authorize]
        public async Task<JsonResult> GetSuppliersForAutoComplete(QueryParameters qParam)
        {
            var query = GetSupplierQuery()
                        .Where(x => x.CompanyName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            if (!string.IsNullOrEmpty(qParam.ExcludedIds))
            {
                var parts = qParam.ExcludedIds.Split(',');
                var ids = new List<int>();
                for (int i = 0; i < parts.Length; i++)
                {
                    int k = 0;
                    if (int.TryParse(parts[i], out k))
                        ids.Add(k);
                }
                query = query.Where(s => !ids.Contains(s.Id));
            }
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.CompanyName,
                           label = x.CompanyName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        protected virtual async Task<List<SupplierDTO>> GetSupplierDtoList(QueryParameters qParams)
        {
            var query = await GetSupplierQuery(qParams);
            var result = qParams.TotalCount > 0 ? await  DataContext.GetSupplierDtoQuery(query).ToListAsync() : new List<SupplierDTO>();

            return result;
        }

        [Authorize]
        public async Task<ActionResult> GetSupplierPagedList(QueryParameters qParams)
        {
            var items = await GetSupplierDtoList(qParams);

            var result = new PagedList<SupplierDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetSupplierList(QueryParameters qParams)
        {
            List<SupplierDTO> result;
            if (qParams.ProductId > 0)
            {
                result = new List<SupplierDTO>();
                var product = await DataContext
                                .Products
                                .Include(p => p.Suppliers)
                                .FirstOrDefaultAsync(p => p.Id == qParams.ProductId);
                if (product != null)
                {
                    foreach (var supplier in product.Suppliers)
                    {
                        result.Add(new SupplierDTO(supplier));
                    }
                }
            }
            else
            {
                result = await GetSupplierDtoList(qParams);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            //qParams.PageSize = 20;
            var items = await GetSupplierDtoList(qParams);
            ViewBag.totalCount = qParams.TotalCount;
            await PutCanUserInViewBag();
            var result = new PagedList<SupplierDTO>(qParams, items);
            ViewBag.result = result.ToJson();
            
            return View();
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

        protected override Task SetSupplierDefaults(Supplier supplier)
        {
            supplier.CountryId = 90;
            return base.SetSupplierDefaults(supplier);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int ProductId { get; set; }
            public string ExcludedIds { get; set; }
        }
    }
}
