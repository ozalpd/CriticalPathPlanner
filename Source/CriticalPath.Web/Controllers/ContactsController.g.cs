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
    public partial class ContactsController : BaseController 
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetContactQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.FirstName.Contains(qParams.SearchString) | 
                            a.LastName.Contains(qParams.SearchString) | 
                            a.EmailWork.Contains(qParams.SearchString) | 
                            a.EmailHome.Contains(qParams.SearchString) | 
                            a.PhoneMobile.Contains(qParams.SearchString) | 
                            a.PhoneWork1.Contains(qParams.SearchString) | 
                            a.PhoneWork2.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CompanyId != null)
            {
                query = query.Where(x => x.CompanyId == qParams.CompanyId);
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
                return View(new List<Contact>());   //there isn't any record, so no need to run a query
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
        public async Task<ActionResult> Details(int? id)  //GET: /Contacts/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await FindAsyncContact(id.Value);

            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }


        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("Contacts/Create/{companyId:int?}")]
        public async Task<ActionResult> Create(int? companyId)  //GET: /Contacts/Create
        {
            var contact = new Contact();
            if (companyId != null)
            {
                var company = await FindAsyncCompany(companyId.Value);
                if (company == null)
                    return HttpNotFound();
                contact.Company = company;
            }
            SetDefaults(contact);
            SetViewBags(null);
            return View(contact);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("Contacts/Create/{companyId:int?}")]
        public async Task<ActionResult> Create(int? companyId, Contact contact)  //POST: /Contacts/Create
        {
            DataContext.SetInsertDefaults(contact, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(contact);
 
                DataContext.Contacts.Add(contact);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(contact);
                return RedirectToAction("Index");
            }

            SetViewBags(contact);
            return View(contact);
        }


        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Contacts/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await FindAsyncContact(id.Value);

            if (contact == null)
            {
                return HttpNotFound();
            }

            SetViewBags(contact);
            return View(contact);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Contact contact)  //POST: /Contacts/Edit/5
        {
            DataContext.SetInsertDefaults(contact, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(contact);
 
                DataContext.Entry(contact).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(contact);
                return RedirectToAction("Index");
            }

            SetViewBags(contact);
            return View(contact);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Contacts/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await FindAsyncContact(id.Value);

            if (contact == null)
            {
                return HttpNotFound();
            }

            DataContext.Contacts.Remove(contact);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(contact.FullName);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? CompanyId { get; set; }
        }

        //Partial methods
        partial void OnCreateSaving(Contact contact);
        partial void OnCreateSaved(Contact contact);
        partial void OnEditSaving(Contact contact);
        partial void OnEditSaved(Contact contact);
        partial void SetDefaults(Contact contact);
        partial void SetViewBags(Contact contact);
    }
}
