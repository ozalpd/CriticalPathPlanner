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
    public partial class ProductsController : BaseController 
    {
        
        public async Task<ActionResult> Index(QueryParameters qParams)
        {
            var query = DataContext.GetProductQuery();
            if (!string.IsNullOrEmpty(qParams.SearchString))
            {
                query = from a in query
                        where
                            a.Title.Contains(qParams.SearchString) | 
                            a.Code.Contains(qParams.SearchString) 
                        select a;
            }
            if (qParams.CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == qParams.CategoryId);
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
                return View(new List<Product>());   //there isn't any record, so no need to run a query
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

        
        public async Task<ActionResult> Details(int? id)  //GET: /Products/Details/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "admin, supervisor, clerk")]
        public ActionResult Create()  //GET: /Products/Create
        {
            var product = new Product();
            SetProductDefaults(product);
            SetSelectLists(null);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin, supervisor, clerk")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)  //POST: /Products/Create
        {
            DataContext.SetInsertDefaults(product, this);

            if (ModelState.IsValid)
            {
                OnCreateSaving(product);
 
                DataContext.Products.Add(product);
                await DataContext.SaveChangesAsync(this);
 
                OnCreateSaved(product);
                return RedirectToAction("Index");
            }

            SetSelectLists(product);
            return View(product);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        public async Task<ActionResult> Edit(int? id)  //GET: /Products/Edit/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            SetSelectLists(product);
            return View(product);
        }

        [Authorize(Roles = "admin, supervisor, clerk")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)  //POST: /Products/Edit/5
        {
            DataContext.SetInsertDefaults(product, this);

            if (ModelState.IsValid)
            {
                OnEditSaving(product);
 
                DataContext.Entry(product).State = EntityState.Modified;
                await DataContext.SaveChangesAsync(this);
 
                OnEditSaved(product);
                return RedirectToAction("Index");
            }

            SetSelectLists(product);
            return View(product);
        }


        [Authorize(Roles = "admin, supervisor")]
        public async Task<ActionResult> Delete(int? id)  //GET: /Products/Delete/5
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await FindAsyncProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            int orderItemsCount = product.OrderItems.Count;
            if ((orderItemsCount) > 0)
            {
                var sb = new StringBuilder();

                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(" <b>");
                sb.Append(product.Title);
                sb.Append("</b>.<br/>");

                if (orderItemsCount > 0)
                {
                    sb.Append(string.Format(MessageStrings.RelatedRecordsExist, orderItemsCount, EntityStrings.OrderItems));
                    sb.Append("<br/>");
                }

                return GetErrorResult(sb, HttpStatusCode.BadRequest);
            }

            DataContext.Products.Remove(product);
            try
            {
                await DataContext.SaveChangesAsync(this);
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                sb.Append(MessageStrings.CanNotDelete);
                sb.Append(product.Title);
                sb.Append("<br/>");
                AppendExceptionMsg(ex, sb);

                return GetErrorResult(sb, HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public new partial class QueryParameters : BaseController.QueryParameters
        {
            public int? CategoryId { get; set; }
        }


        //Partial methods
        partial void OnCreateSaving(Product product);
        partial void OnCreateSaved(Product product);
        partial void OnEditSaving(Product product);
        partial void OnEditSaved(Product product);
        partial void SetSelectLists(Product product);
    }
}
