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
    public partial class SuppliersController 
    {

        protected override Task SetSupplierDefaults(Supplier supplier)
        {
            return base.SetSupplierDefaults(supplier);
        }
    }
}
