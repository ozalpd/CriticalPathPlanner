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
        public async Task<ActionResult> Details(int? id)  //GET: /Countries/Details/5
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
            return View(country);
        }

        [Authorize]
        public async Task<ActionResult> GetCountry(int? id)
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

            return Json(new CountryDTO(country), JsonRequestBehavior.AllowGet);
        }


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
