using CP.i8n;
using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CriticalPath.Web.Models
{
    public class DiscontinueCompanyVM : DiscontinuedVM
    {
        public DiscontinueCompanyVM() { }
        public DiscontinueCompanyVM(Company company) : base(company)
        {
            Id = company.Id;
            CompanyName = company.CompanyName;
            Country = company.Country != null ? company.Country.CountryName : string.Empty;
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "CompanyName")]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Country")]
        public string Country { get; set; }
    }
}