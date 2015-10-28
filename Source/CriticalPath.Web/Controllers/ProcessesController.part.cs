using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using CriticalPath.Data;
using CriticalPath.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class ProcessesController
    {
        protected virtual IQueryable<Process> GetProcessQuery(QueryParameters qParams)
        {
            var query = GetProcessQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) |
                            a.Description.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.ProcessTemplateId != null)
            {
                query = query.Where(x => x.ProcessTemplateId == qParams.ProcessTemplateId);
            }
            if (qParams.OrderItemId != null)
            {
                query = query.Where(x => x.OrderItemId == qParams.OrderItemId);
            }

            return query;
        }

        protected override async Task PutCanUserInViewBag()
        {
            ViewBag.canUserApprove = await CanUserApprove();
            await base.PutCanUserInViewBag();
        }

        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(int? id)  //GET: /PurchaseOrders/Edit/5
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var process = await FindAsyncProcess(id.Value);

            if (process == null)
                return HttpNotFound();

            if (process.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(process);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(ProcessDTO vm)
        {
            var process = await FindAsyncProcess(vm.Id);
            if (vm.IsApproved && process != null)
            {
                await ApproveSaveAsync(process);
                return RedirectToAction("Details", new { id = process.Id });
            }

            return View(process);
        }


        partial void SetSelectLists(Process process)
        {
            var queryTemplateId = DataContext
                                    .GetProcessTemplateDtoQuery()
                                    .Where(t => t.IsApproved);
            int processTemplateId = process == null ? 0 : process.ProcessTemplateId;
            ViewBag.ProcessTemplateId = new SelectList(queryTemplateId, "Id", "TemplateName", processTemplateId);
        }

        partial void OnCreateSaving(Process process)
        {
            var queryTemplate = DataContext.GetProcessStepTemplateQuery();
            DateTime targetDate = process.TargetStartDate;
            DateTime forecastDate = process.ForecastStartDate.HasValue ?
                                    process.ForecastStartDate.Value : DateTime.MinValue;

            foreach (var template in queryTemplate)
            {
                var step = new ProcessStep()
                {
                    Title = template.Title,
                    DisplayOrder = template.DisplayOrder,
                    TemplateId = template.Id,
                    TargetStartDate = targetDate
                };
                targetDate =  targetDate.AddDays(template.RequiredWorkDays);
                step.TargetEndDate = targetDate;
                targetDate = targetDate.AddDays(1);
                if (process.ForecastStartDate.HasValue)
                {
                    step.ForecastStartDate = forecastDate;
                    forecastDate = forecastDate.AddDays(template.RequiredWorkDays);
                    step.ForecastEndDate = forecastDate;
                    forecastDate = forecastDate.AddDays(1);
                }
                process.ProcessSteps.Add(step);
            }
        }

        protected override Task SetProcessDefaults(Process process)
        {
            process.TargetStartDate = DateTime.Today;
            return Task.FromResult(default(object));
        }
    }
}
