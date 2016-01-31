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
    public partial class EmployeePositionsController : BaseController 
    {
        protected virtual async Task<IQueryable<EmployeePosition>> GetEmployeePositionQuery(QueryParameters qParams)
        {
            var query = GetEmployeePositionQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Position.Contains(qParams.SearchString) 
                        select a;
            }

            qParams.TotalCount = await query.CountAsync();
            return query.Skip(qParams.Skip).Take(qParams.PageSize);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = await GetEmployeePositionQuery(qParams);
            await PutCanUserInViewBag();
			var result = new PagedList<EmployeePosition>(qParams);
            if (qParams.TotalCount > 0)
            {
                result.Items = await query.ToListAsync();
            }

            PutPagerInViewBag(result);
            return View(result.Items);
        }

        protected override async Task<bool> CanUserCreate()
        {
            if (!_canUserCreate.HasValue)
            {
                _canUserCreate = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserCreate.Value;
        }
        bool? _canUserCreate;

        protected override async Task<bool> CanUserEdit()
        {
            if (!_canUserEdit.HasValue)
            {
                _canUserEdit = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserEdit.Value;
        }
        bool? _canUserEdit;
        
        protected override async Task<bool> CanUserDelete()
        {
            if (!_canUserDelete.HasValue)
            {
                _canUserDelete = Request.IsAuthenticated && (
                                    await IsUserAdminAsync());
            }
            return _canUserDelete.Value;
        }
        bool? _canUserDelete;

        
        protected override Task<bool> CanUserSeeRestricted() { return Task.FromResult(true); }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Details(int? id)  //GET: /EmployeePositions/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePosition employeePosition = await FindAsyncEmployeePosition(id.Value);

            if (employeePosition == null)
            {
                return HttpNotFound();
            }

            await PutCanUserInViewBag();
            return View(employeePosition);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()  //GET: /EmployeePositions/Create
        {
            var employeePosition = new EmployeePosition();
            await SetEmployeePositionDefaults(employeePosition);
            SetSelectLists(employeePosition);
            return View(employeePosition);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeePosition employeePosition)  //POST: /EmployeePositions/Create
        {
            if (ModelState.IsValid)
            {
                OnCreateSaving(employeePosition);
 
                DataContext.EmployeePositions.Add(employeePosition);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(employeePosition);
                return RedirectToAction("Index");
            }

            SetSelectLists(employeePosition);
            return View(employeePosition);
        }


        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public QueryParameters() { }
            public QueryParameters(QueryParameters parameters) : base(parameters)
            {
            }
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
        partial void OnCreateSaving(EmployeePosition employeePosition);
        partial void OnCreateSaved(EmployeePosition employeePosition);
        partial void SetSelectLists(EmployeePosition employeePosition);
    }
}
