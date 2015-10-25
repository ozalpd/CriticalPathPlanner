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
    public partial class ContactsController
    {
        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams) //mod: Search by CompanyName
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
                            a.Company.CompanyName.Contains(qParams.SearchString)
                        select a;
            }
            if (qParams.CompanyId != null)
            {
                query = query.Where(x => x.CompanyId == qParams.CompanyId);
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
                return View(new List<Contact>());   //there isn't any record, so no need to run a query
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


        //partial void SetSelectLists(Contact contact)
        //{
        //TODO: Optimize query
        //var queryCompanyId = DataContext.Companies;
        //int companyId = contact == null ? 0 : contact.CompanyId;
        //ViewBag.CompanyId = new SelectList(queryCompanyId, "Id", "CompanyName", companyId);
        //}


        //Purpose: To set default property values for newly created Contact entity
        //protected override void SetContactDefaults(Contact contact) { }
    }
}
