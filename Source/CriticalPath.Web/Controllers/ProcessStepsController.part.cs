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
using CriticalPath.Data.Resources;

namespace CriticalPath.Web.Controllers
{
    public partial class ProcessStepsController
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetProcessStepQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Process.Title.Contains(qParams.SearchString) |
                            a.Process.Description.Contains(qParams.SearchString) |
                            a.Process.PurchaseOrder.Product.Title.Contains(qParams.SearchString) |
                            a.Process.PurchaseOrder.Product.Code.Contains(qParams.SearchString) |
                            a.Title.Contains(qParams.SearchString) |
                            a.Description.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.ProcessId != null)
            {
                query = query.Where(x => x.ProcessId == qParams.ProcessId);
            }
            if (qParams.TemplateId != null)
            {
                query = query.Where(x => x.TemplateId == qParams.TemplateId);
            }
            qParams.TotalCount = await query.CountAsync();
            PutPagerInViewBag(qParams);
            await PutCanUserInViewBag();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<ProcessStep>());   //there isn't any record, so no need to run a query
            }
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
    }
}
