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
    public partial class ContactsController
    {
        protected virtual async Task<IQueryable<Contact>> GetContactQuery(QueryParameters qParams)
        {
            var query = GetContactQuery();
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
            return query;
        }

        protected override Task SetContactDefaults(Contact contact)
        {
            return base.SetContactDefaults(contact);
        }
    }
}
