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
    public partial class SuppliersController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetSupplierQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.CompanyName.Contains(qParams.SearchString) | 
                            a.SupplierCode.Contains(qParams.SearchString) | 
                            a.Phone1.Contains(qParams.SearchString) | 
                            a.Phone2.Contains(qParams.SearchString) | 
                            a.Phone3.Contains(qParams.SearchString) | 
                            a.Address1.Contains(qParams.SearchString) | 
                            a.Address2.Contains(qParams.SearchString) 
                        select a;
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
                return View(new List<Supplier>());   //there isn't any record, so no need to run a query
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

        [Authorize]
        public async Task<ActionResult> Details(int? id)  //GET: /Suppliers/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /Suppliers/Create
        {
            var supplier = new Supplier();
            SetSupplierDefaults(supplier);
            SetSelectLists(null);
            return View(supplier);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Supplier supplier)  //POST: /Suppliers/Create
        {
            DataContext.SetInsertDefaults(supplier, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(supplier);
 
                DataContext.Companies.Add(supplier);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(supplier);
                return RedirectToAction("Index");
            }

            SetSelectLists(supplier);
            return View(supplier);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Suppliers/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(supplier);
            return View(supplier);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Supplier supplier)  //POST: /Suppliers/Edit/5
        {
            DataContext.SetInsertDefaults(supplier, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(supplier);
 
                DataContext.Entry(supplier).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(supplier);
                return RedirectToAction("Index");
            }

            SetSelectLists(supplier);
            return View(supplier);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Suppliers/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = await FindAsyncSupplier(id.Value);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            int contactsCount = supplier.Contacts.Count;
            if ((contactsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(supplier.CompanyName);
                sb.Append("</b>.<br/>");

                if (contactsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, contactsCount, EntityStrings.Contacts));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Companies.Remove(supplier);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(supplier.CompanyName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        //Partial methods
        partial void OnCreateSaving(Supplier supplier);
        partial void OnCreateSaved(Supplier supplier);
        partial void OnEditSaving(Supplier supplier);
        partial void OnEditSaved(Supplier supplier);
        partial void SetSelectLists(Supplier supplier);
    }
}
