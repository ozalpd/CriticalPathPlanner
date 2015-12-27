using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CP.i8n;
using CriticalPath.Web.Areas.Admin.Models;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class CurrenciesController
    {
        protected override IQueryable<Currency> GetCurrencyQuery()
        {
            return DataContext.Currencies.OrderBy(c => c.CurrencyCode);
        }

        protected virtual async Task<IQueryable<Currency>> GetCurrencyQuery(QueryParameters qParams)
        {
            var query = GetCurrencyQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CurrencyName.Contains(qParams.SearchString) |
                            a.CurrencyCode.Contains(qParams.SearchString) |
                            a.CurrencySymbol.Contains(qParams.SearchString)
                        select a;
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()  //GET: /Currencies/Create
        {
            var currency = new Currency();
            await SetCurrencyDefaults(currency);
            SetSelectLists(currency);
            return View(currency);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Currency currency)  //POST: /Currencies/Create
        {
            DataContext.SetInsertDefaults(currency, this);

            if (ModelState.IsValid)
            {
                DataContext.Currencies.Add(currency);
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshCurrencyDtoList();
                return RedirectToAction("Index");
            }

            SetSelectLists(currency);
            return View(currency);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Currencies/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await FindAsyncCurrency(id.Value);

            if (currency == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(currency);
            return View(new CurrencyEditVM(currency));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CurrencyEditVM vm)
        {
            Currency currency = await FindAsyncCurrency(vm.Id);

            if (ModelState.IsValid)
            {
                currency.CurrencyName = vm.CurrencyName;
                currency.CurrencySymbol = vm.CurrencySymbol;
                currency.IsPublished = vm.IsPublished;

                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshCurrencyDtoList();
                return RedirectToAction("Index");
            }

            SetSelectLists(currency);
            return View(vm);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Currencies/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await FindAsyncCurrency(id.Value);

            if (currency == null)
            {
                return HttpNotFound();
            }

            DataContext.Currencies.Remove(currency);
            try
            {
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshCurrencyDtoList();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(currency.CurrencyCode);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetAjaxStatusCode(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
