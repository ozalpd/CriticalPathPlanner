using CriticalPath.Web.Controllers;
using CriticalPath.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = SecurityRoles.Admin)]
    public partial class SetupController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.status = await GetRecordCounts();
            return View();
        }

        public async Task<ActionResult> SetDefaultData()
        {
            var sb = new StringBuilder();
            sb.Append("<h1>Setting Up Default Data</h1>");

            await SeedSizingStandards(sb);
            await DataContext.SaveChangesAsync(this);

            ViewBag.status = sb.ToString();
            return View("Index");
        }


        private async Task<string> GetRecordCounts()
        {
            var sb = new StringBuilder();

            int count = 0;

            count = await DataContext.GetSizingStandardQuery().CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" SizingStandard records.<br>");

            count = await DataContext.GetCustomerQuery().CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Customer records.<br>");

            count = await DataContext.GetSupplierQuery().CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Supplier records.<br>");

            count = await DataContext.Contacts.CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Contact records.<br>");

            count = await DataContext.ProductCategories.CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" ProductCategory records.<br>");

            count = await DataContext.Products.CountAsync();
            sb.Append("Database has ");
            sb.Append(count);
            sb.Append(" Product records.<br>");

            return sb.ToString();
        }
    }
}