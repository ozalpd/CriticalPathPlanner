using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public partial class PurchaseOrderVM : PurchaseOrderDTO
    {
        public PurchaseOrderVM(PurchaseOrder entity) : base(entity) { }
        public PurchaseOrderVM() { }

        protected override void Constructing(PurchaseOrder entity)
        {
            base.Constructing(entity);
            Customer = entity.Customer != null ? new CustomerDTO(entity.Customer) : null;
            Product = entity.Product != null ? new ProductDTO(entity.Product) : null;
            SizingStandard = entity.SizingStandard != null ? new SizingStandardDTO(entity.SizingStandard) : null;
        }

        public CustomerDTO Customer { get; set; }
        public ProductDTO Product { get; set; }
        public SizingStandardDTO SizingStandard { get; set; }
    }
}
