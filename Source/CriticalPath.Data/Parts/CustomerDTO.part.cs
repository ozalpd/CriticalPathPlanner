using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CustomerDTO
    {
        partial void Initiliazing(Customer entity)
        {
            Country = entity.Country != null ? entity.Country.CountryName : string.Empty;
        }

        [Display(ResourceType = typeof(EntityStrings), Name = "Country")]
        public string Country { get; set; }
    }
}
