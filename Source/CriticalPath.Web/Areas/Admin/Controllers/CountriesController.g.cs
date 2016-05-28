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
using CriticalPath.Web.Controllers;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class CountriesController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> GetCountryList(QueryParameters qParams)
        {
            var result = await GetCountryDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetCountryPagedList(QueryParameters qParams)
        {
            var items = await GetCountryDtoList(qParams);
            var result = new PagedList<CountryDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetCountriesForAutoComplete(QueryParameters qParam)
        {
            var query = GetCountryQuery()
                        .Where(x => x.CountryName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.CountryName,
                           label = x.CountryName
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
            Country country = await FindAsyncCountry(id.Value);

            if (country == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", country);
            }
            return View(country);
        }

        [Authorize]
        public async Task<ActionResult> GetCountry(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Country country = await FindAsyncCountry(id.Value);

            if (country == null)
            {
                return NotFoundTextResult();
            }

            return Json(new CountryDTO(country), JsonRequestBehavior.AllowGet);
        }


        protected override bool CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor());
            }
            return _canUserCreate.Value;
        }
        protected override async Task<bool> CanUserCreateAsync()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
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
                                    IsUserSupervisor());
            }
            return _canUserEdit.Value;
        }
        protected override async Task<bool> CanUserEditAsync()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;
        
        protected override bool CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    IsUserAdmin());
            }
            return _canUserDelete.Value;
        }
        protected override async Task<bool> CanUserDeleteAsync()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
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
            }
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
        partial void SetSelectLists(Country country);
    }
}
