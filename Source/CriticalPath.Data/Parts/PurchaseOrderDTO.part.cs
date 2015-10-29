using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class PurchaseOrderDTO : IIsApproved, IHasProduct
    {
        public ProductDTO Product { get; set; }

        partial void Initiliazing(PurchaseOrder entity)
        {
            Constructing(entity);
        }

        /// <summary>
        /// Constructing with a PurchaseOrder instance
        /// </summary>
        /// <param name="entity">PurchaseOrder instance</param>
        protected virtual void Constructing(PurchaseOrder entity)
        {
            if (entity.Product != null)
            {
                Product = new ProductDTO(entity.Product);
            }
        }

        partial void Converting(PurchaseOrder entity)
        {
            if (Product != null)
            {
                entity.Product = Product.ToProduct();
            }
        }
    }
}
