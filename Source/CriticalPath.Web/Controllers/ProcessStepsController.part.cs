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
    public partial class ProcessStepsController
    {
        protected void PutPagerInViewBag(QueryParameters qParams)
        {
            base.PutPagerInViewBag(qParams);
            ViewBag.processId = qParams.ProcessId ?? 0;
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

            var processStep = await FindAsyncProcessStep(id.Value);

            if (processStep == null)
                return HttpNotFound();

            if (processStep.IsApproved)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(processStep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Approve(ProcessStepDTO vm)
        {
            var processStep = await FindAsyncProcessStep(vm.Id);
            if (vm.IsApproved && processStep != null)
            {
                await ApproveSaveAsync(processStep);
                return RedirectToAction("Details", new { id = processStep.Id });
            }

            return View(processStep);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> SetProcessStepDate(int? id, DateTime? realizedDate, DateTime? targetDate, DateTime? forecastDate)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = await FindAsyncProcessStep(id.Value);

            if (processStep == null)
            {
                return HttpNotFound();
            }

            if (realizedDate.HasValue)
                processStep.RealizedDate = realizedDate.Value;

            if (targetDate.HasValue)
                processStep.TargetDate = targetDate.Value;

            if (forecastDate.HasValue)
                processStep.ForecastDate = forecastDate.Value;

            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(processStep.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
