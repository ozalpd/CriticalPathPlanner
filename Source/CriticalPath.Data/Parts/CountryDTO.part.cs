using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CountryDTO
    {
        partial void Initiliazing(Country entity)
        {
            IsPublished = entity.IsPublished;
        }

        partial void Converting(Country entity)
        {
            entity.IsPublished = IsPublished;
        }

        [Display(ResourceType = typeof(EntityStrings), Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
