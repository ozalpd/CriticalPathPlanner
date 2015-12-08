using CP.i8n;
using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class PurchaseOrderCreateVM : PurchaseOrderEditVM
    {
        public PurchaseOrderCreateVM(PurchaseOrder entity) : base(entity) { }
        public PurchaseOrderCreateVM() { }
        protected override void Constructing(PurchaseOrder entity)
        {
            base.Constructing(entity);
            if (Customer != null)
            {
                CustomerName = Customer.CompanyName;
            }
        }

        [StringLength(64, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "ProductCode")]
        public string ProductCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(EntityStrings), Name = "Customer")]
        public string CustomerName { get; set; }
    }
}
