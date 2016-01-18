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
    public partial class LicensorsController 
    {
        partial void SetSelectLists(Licensor licensor)
        {
            //TODO: Optimize query
            var queryCountryId = DataContext.Countries;
            int countryId = licensor == null ? 0 : licensor.CountryId;
            ViewBag.CountryId = new SelectList(queryCountryId, "Id", "CountryName", countryId);
        }


        //Purpose: To set default property values for newly created Licensor entity
        //protected override async Task SetLicensorDefaults(Licensor licensor) { }
    }
}
