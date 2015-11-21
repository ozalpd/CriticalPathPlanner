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
    public partial class SuppliersController
    {
        protected virtual async Task<List<SupplierDTO>> GetSupplierDtoList(QueryParameters qParams)
        {
            var query = await GetSupplierQuery(qParams);
            var result = qParams.TotalCount > 0 ? await  DataContext.GetSupplierDtoQuery(query).ToListAsync() : new List<SupplierDTO>();

            return result;
        }

        [Authorize]
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            qParams.PageSize = 20;
            var items = await GetSupplierDtoList(qParams);
            ViewBag.totalCount = qParams.TotalCount;
            await PutCanUserInViewBag();
            var result = new PagedList<SupplierDTO>(qParams, items);
            ViewBag.result = result.ToJson();
            
            return View();
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

        protected override Task SetSupplierDefaults(Supplier supplier)
        {
            return base.SetSupplierDefaults(supplier);
        }
    }
}
