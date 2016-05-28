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
using OzzUtils.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public partial class CustomersController
    {
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<JsonResult> GetCustomersForAutoComplete(QueryParameters qParam)
        {
            var query = DataContext.GetCustomerDtoQuery(GetCustomerQuery()
                        .Where(x => x.CompanyName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize));
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.CompanyName,
                           label = x.CompanyName + " [" + x.Country + " / " + x.City + "]",
                           DiscountRate = x.DiscountRate
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        protected virtual async Task<List<CustomerDTO>> GetCustomerDtoList(QueryParameters qParams)
        {
            var query = await GetCustomerQuery(qParams);
            var result = qParams.TotalCount > 0 ?
                        await DataContext.GetCustomerDtoQuery(query).ToListAsync() :
                        new List<CustomerDTO>();

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            //qParams.PageSize = 20;
            var items = await GetCustomerDtoList(qParams);
            ViewBag.totalCount = qParams.TotalCount;
            await PutCanUserInViewBag();
            var result = new PagedList<CustomerDTO>(qParams, items);
            ViewBag.result = result.ToJson();

            return View();
        }


        protected override Task SetCustomerDefaults(Customer customer)
        {
            customer.CountryId = 44;
            return base.SetCustomerDefaults(customer);
        }
    }
}
