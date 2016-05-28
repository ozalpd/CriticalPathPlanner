using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using OzzUtils.Web.Mvc;
using System;
using System.Net;
using System.Text;
using CP.i8n;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class CountriesController
    {
        protected override IQueryable<Country> GetCountryQuery()
        {
            return DataContext.GetCountryQuery(false);
        }

        protected virtual async Task<IQueryable<Country>> GetCountryQuery(QueryParameters qParams)
        {
            var query = GetCountryQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CountryName.Contains(qParams.SearchString) |
                            a.TwoLetterIsoCode.Contains(qParams.SearchString) |
                            a.ThreeLetterIsoCode.Contains(qParams.SearchString)
                        select a;
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        protected virtual async Task<List<CountryDTO>> GetCountryDtoList(QueryParameters qParams)
        {
            var query = await GetCountryQuery(qParams);
            var result = qParams.TotalCount > 0 ? 
                            await DataContext.GetCountryDtoQuery(query).ToListAsync() :
                            new List<CountryDTO>();

            return result;
        }


        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetCountryQuery(qParams);
            await PutCanUserInViewBag();
            var result = new PagedList<CountryDTO>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await DataContext.GetCountryDtoQuery(query).ToListAsync();
            }
            ViewBag.result = result.ToJson();
            //PutPagerInViewBag(result);
            return View(result.Items);
        }


        [HttpGet]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Create()  //GET: /Countries/Create
        {
            var country = new Country();
            await SetCountryDefaults(country);
            return View(country);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Country country)  //POST: /Countries/Create
        {
            if (ModelState.IsValid)
            {
                DataContext.Countries.Add(country);
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshCountryDtoList();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Countries/Edit/5
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

            return View(country);
        }

        [Authorize(Roles = "admin, supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Country country)  //POST: /Countries/Edit/5
        {
            if (ModelState.IsValid)
            {
                DataContext.Entry(country).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshCountryDtoList();

                return RedirectToAction("Index");
            }

            return View(country);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Countries/Delete/5
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

            DataContext.Countries.Remove(country);
            try
            {
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshCountryDtoList();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(country.CountryName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
