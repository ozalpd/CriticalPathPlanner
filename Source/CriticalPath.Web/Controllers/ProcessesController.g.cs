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
    public partial class ProcessesController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetProcessQuery();
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
            qParams.TotalCount = await query.CountAsync();
            SetPagerParameters(qParams);

            ViewBag.canUserEdit = await CanUserEdit();
            ViewBag.canUserCreate = await CanUserCreate();
            ViewBag.canUserDelete = await CanUserDelete();

            if (qParams.TotalCount > 0)
            {
                return View(await query.Skip(qParams.Skip).Take(qParams.PageSize).ToListAsync());
            }
            else
            {
                return View(new List<Process>());   //there isn't any record, so no need to run a query
            }
        }
        
        protected virtual async Task<bool> CanUserCreate()
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

        protected virtual async Task<bool> CanUserEdit()
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
        
        protected virtual async Task<bool> CanUserDelete()
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


        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /Processes/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            return View(process);
        }


        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("Processes/Create/{orderItemId:int?}")]
        public async Task<ActionResult> Create(int? orderItemId)  //GET: /Processes/Create
        {
            var process = new Process();
            if (orderItemId != null)
            {
                var orderItem = await FindAsyncOrderItem(orderItemId.Value);
                if (orderItem == null)
                    return HttpNotFound();
                process.OrderItem = orderItem;
            }
            SetDefaults(process);
            SetViewBags(null);
            return View(process);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("Processes/Create/{orderItemId:int?}")]
        public async Task<ActionResult> Create(int? orderItemId, Process process)  //POST: /Processes/Create
        {
            DataContext.SetInsertDefaults(process, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(process);
 
                DataContext.Processes.Add(process);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(process);
                return RedirectToAction("Index");
            }

            SetViewBags(process);
            return View(process);
        }


        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Processes/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            SetViewBags(process);
            return View(process);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Process process)  //POST: /Processes/Edit/5
        {
            DataContext.SetInsertDefaults(process, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(process);
 
                DataContext.Entry(process).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(process);
                return RedirectToAction("Index");
            }

            SetViewBags(process);
            return View(process);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Processes/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await FindAsyncProcess(id.Value);

            if (process == null)
            {
                return HttpNotFound();
            }

            int processStepsCount = process.ProcessSteps.Count;
            if ((processStepsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(process.Title);
                sb.Append("</b>.<br/>");

                if (processStepsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, processStepsCount, EntityStrings.ProcessSteps));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Processes.Remove(process);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(process.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? ProcessTemplateId { get; set; }
            public int? OrderItemId { get; set; }
        }

        //Partial methods
        partial void OnCreateSaving(Process process);
        partial void OnCreateSaved(Process process);
        partial void OnEditSaving(Process process);
        partial void OnEditSaved(Process process);
        partial void SetDefaults(Process process);
        partial void SetViewBags(Process process);
    }
}
