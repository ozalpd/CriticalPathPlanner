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
    public partial class ManufacturersController 
    {
        //partial void SetSelectLists(Manufacturer manufacturer)
        //{
        //    //TODO: Optimize query
        //    var querySupplierId = DataContext.Companies.OfType<Supplier>();
        //    int supplierId = manufacturer == null ? 0 : manufacturer.SupplierId;
        //    ViewBag.SupplierId = new SelectList(querySupplierId, "Id", "CompanyName", supplierId);
        //    //TODO: Optimize query
        //    var queryCountryId = DataContext.Countries;
        //    int countryId = manufacturer == null ? 0 : manufacturer.CountryId;
        //    ViewBag.CountryId = new SelectList(queryCountryId, "Id", "CountryName", countryId);
        //}

        protected override Task SetManufacturerDefaults(Manufacturer manufacturer)
        {
            manufacturer.CountryId = manufacturer.Supplier == null ? 90 : manufacturer.Supplier.CountryId;
            return base.SetManufacturerDefaults(manufacturer);
        }

        //Purpose: To set default property values for newly created Manufacturer entity
        //protected override async Task SetManufacturerDefaults(Manufacturer manufacturer) { }
    }
}
