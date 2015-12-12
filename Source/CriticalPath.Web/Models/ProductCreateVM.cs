using CP.i8n;
using CriticalPath.Data;
using System.ComponentModel.DataAnnotations;

namespace CriticalPath.Web.Models
{
    public class ProductCreateVM : ProductEditVM
    {
        public ProductCreateVM() { }
        public ProductCreateVM(Product entity) : base(entity) { }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "Supplier")]
        public string SupplierName { get; set; }
    }
}