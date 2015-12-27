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
    public partial class FreightTermsController
    {
        protected override IQueryable<FreightTerm> GetFreightTermQuery()
        {
            return DataContext.GetFreightTermQuery(false);
        }

        protected virtual async Task<IQueryable<FreightTerm>> GetFreightTermQuery(QueryParameters qParams)
        {
            var query = DataContext.GetFreightTermQuery(false);
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.IncotermCode.Contains(qParams.SearchString) |
                            a.Description.Contains(qParams.SearchString)
                        select a;
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()  //GET: /FreightTerms/Create
        {
            var freightTerm = new FreightTerm();
            await SetFreightTermDefaults(freightTerm);
            SetSelectLists(freightTerm);
            return View(freightTerm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FreightTerm freightTerm)  //POST: /FreightTerms/Create
        {
            DataContext.SetInsertDefaults(freightTerm, this);

            if (ModelState.IsValid)
            {
                DataContext.FreightTerms.Add(freightTerm);
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshFreightTermDtoList();

                return RedirectToAction("Index");
            }

            SetSelectLists(freightTerm);
            return View(freightTerm);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int? id)  //GET: /FreightTerms/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightTerm freightTerm = await FindAsyncFreightTerm(id.Value);

            if (freightTerm == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(freightTerm);
            return View(new FreightTermEditVM(freightTerm));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FreightTermEditVM vm)  //POST: /FreightTerms/Edit/5
        {
            FreightTerm freightTerm = await FindAsyncFreightTerm(vm.Id);
            if (freightTerm == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                freightTerm.IsPublished = vm.IsPublished;
                freightTerm.Description = vm.Description;
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshFreightTermDtoList();

                return RedirectToAction("Index");
            }

            SetSelectLists(freightTerm);
            return View(vm);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)  //GET: /FreightTerms/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FreightTerm freightTerm = await FindAsyncFreightTerm(id.Value);

            if (freightTerm == null)
            {
                return HttpNotFound();
            }

            DataContext.FreightTerms.Remove(freightTerm);
            try
            {
                await DataContext.SaveChangesAsync(this);
                await DataContext.RefreshFreightTermDtoList();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(freightTerm.IncotermCode);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetAjaxStatusCode(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
