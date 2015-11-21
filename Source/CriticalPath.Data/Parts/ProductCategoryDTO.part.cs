using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    partial class ProductCategoryDTO
    {
        partial void Initiliazing(ProductCategory entity)
        {
            ParentCategory = entity.ParentCategory == null ? null : new ProductCategoryDTO(entity.ParentCategory);
        }

        [Display(ResourceType = typeof(EntityStrings), Name = "ParentCategory")]
        public ProductCategoryDTO ParentCategory { get; set; }
    }
}
