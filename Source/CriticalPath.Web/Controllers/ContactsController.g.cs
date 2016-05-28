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
    public partial class ContactsController : BaseController 
    {
        protected virtual async Task<List<ContactDTO>> GetContactDtoList(QueryParameters qParams)
        {
            var query = await GetContactQuery(qParams);
            var list = qParams.TotalCount > 0 ? await query.ToListAsync() : new List<Contact>();
            var result = new List<ContactDTO>();
            foreach (var item in list)
            {
                result.Add(new ContactDTO(item));
            }

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            await PutCanUserInViewBag();
            var query = await GetContactQuery(qParams);
            var result = new PagedList<Contact>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

        [Authorize]
        public async Task<ActionResult> GetContactList(QueryParameters qParams)
        {
            var result = await GetContactDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> GetContactPagedList(QueryParameters qParams)
        {
            var items = await GetContactDtoList(qParams);
            var result = new PagedList<ContactDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> GetContactsForAutoComplete(QueryParameters qParam)
        {
            var query = GetContactQuery()
                        .Where(x => x.FullName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.FullName,
                           label = x.FullName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id, bool? modal)
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

            await PutCanUserInViewBag();
            if (modal ?? false)
            {
                return PartialView("_Details", contact);
            }
            return View(contact);
        }

        [Authorize]
        public async Task<ActionResult> GetContact(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Contact contact = await FindAsyncContact(id.Value);

            if (contact == null)
            {
                return NotFoundTextResult();
            }

            return Json(new ContactDTO(contact), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [Route("Contacts/Create/{companyId:int?}")]
        public async Task<ActionResult> Create(int? companyId, bool? modal)
        {
            var contact = new Contact();
            if (companyId != null)
            {
                var company = await FindAsyncCompany(companyId.Value);
                if (company == null)
                    return HttpNotFound();
                contact.Company = company;
                contact.CompanyId = companyId.Value;
            }
            await SetContactDefaults(contact);
            SetSelectLists(contact);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", contact);
            }
            return View(contact);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        [Route("Contacts/Create/{companyId:int?}")]
        public async Task<ActionResult> Create(int? companyId, Contact contact, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(contact);
 
                DataContext.Contacts.Add(contact);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(contact);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            SetSelectLists(contact);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Create", contact);
            }
            return View(contact);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id, bool? modal)
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

            SetSelectLists(contact);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", contact);
            }
            return View(contact);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Contact contact, bool? modal)
        {
            if (ModelState.IsValid)
            {
                OnEditSaving(contact);
 
                DataContext.Entry(contact).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(contact);
                if (modal ?? false)
                {
                    return Json(new { saved = true });
                }
                return RedirectToAction("Index");
            }

            SetSelectLists(contact);
            if (modal ?? false)
            {
                ViewBag.Modal = true;
                return PartialView("_Edit", contact);
            }
            return View(contact);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Contacts
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Contact contact = await FindAsyncContact(id.Value);

            if (contact == null)
            {
                return NotFoundTextResult();
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

                return StatusCodeTextResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override bool CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserCreate.Value;
        }
        protected override async Task<bool> CanUserCreateAsync()
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

        protected override bool CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canUserEdit.Value;
        }
        protected override async Task<bool> CanUserEditAsync()
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
        
        protected override bool CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor());
            }
            return _canUserDelete.Value;
        }
        protected override async Task<bool> CanUserDeleteAsync()
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

        protected override bool CanUserSeeRestricted()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    IsUserAdmin() ||
                                    IsUserSupervisor() ||
                                    IsUserClerk());
            }
            return _canSeeRestricted.Value;
        }
        protected override async Task<bool> CanUserSeeRestrictedAsync()
        {
            if (!_canSeeRestricted.HasValue)
            {
                _canSeeRestricted = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canSeeRestricted.Value;
        }
        bool? _canSeeRestricted;



        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                CompanyId = parameters.CompanyId;
            }
            public int? CompanyId { get; set; }
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
        partial void OnCreateSaving(Contact contact);
        partial void OnCreateSaved(Contact contact);
        partial void OnEditSaving(Contact contact);
        partial void OnEditSaved(Contact contact);
        partial void SetSelectLists(Contact contact);
    }
}
