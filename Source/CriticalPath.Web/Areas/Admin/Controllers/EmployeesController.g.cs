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
using CriticalPath.Web.Controllers;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class EmployeesController : BaseController 
    {
        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetEmployeeList(QueryParameters qParams)
        {
            var result = await GetEmployeeDtoList(qParams);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetEmployeePagedList(QueryParameters qParams)
        {
            var items = await GetEmployeeDtoList(qParams);
            var result = new PagedList<EmployeeDTO>(qParams, items);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<JsonResult> GetEmployeesForAutoComplete(QueryParameters qParam)
        {
            var query = GetEmployeeQuery()
                        .Where(x => x.AspNetUser.UserName.Contains(qParam.SearchString))
                        .Take(qParam.PageSize);
            var list = from x in query
                       select new
                       {
                           id = x.Id,
                           value = x.AspNetUser.UserName,
                           label = x.AspNetUser.UserName
                       };

            return Json(await list.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> GetEmployee(int? id)
        {
            if (id == null)
            {
                return BadRequestTextResult();
            }
            Employee employee = await FindAsyncEmployee(id.Value);

            if (employee == null)
            {
                return NotFoundTextResult();
            }

            return Json(new EmployeeDTO(employee), JsonRequestBehavior.AllowGet);
        }


        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
                PositionId = parameters.PositionId;
            }
            public int? PositionId { get; set; }
            public bool? IsActive { get; set; }
            public DateTime? InactivateDateMin { get; set; }
            public DateTime? InactivateDateMax { get; set; }
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
        partial void SetSelectLists(Employee employee);
    }
}
