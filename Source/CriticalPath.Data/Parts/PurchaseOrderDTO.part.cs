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
            foreach (var rate in entity.SizeRates)
            {
                SizeRates.Add(new SizeRateDTO(rate));
            }
        }

        partial void Converting(PurchaseOrder entity)
        {
            if (Product != null)
            {
                entity.Product = Product.ToProduct();
            }
            foreach (var rate in SizeRates)
            {
                entity.SizeRates.Add(rate.ToSizeRate());
            }
        }


        public ICollection<SizeRateDTO> SizeRates
        {
            set { _sizeRates = value; }
            get
            {
                if (_sizeRates == null)
                    _sizeRates = new List<SizeRateDTO>();
                return _sizeRates;
            }
        }
        private ICollection<SizeRateDTO> _sizeRates;

    }
}
