using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class PurchaseOrderDTO : IIsApproved, IHasProduct, ICancelled, IAllPrice
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
            foreach (var rate in entity.SizeRatios)
            {
                SizeRates.Add(new SizeRatioDTO(rate));
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
                entity.SizeRatios.Add(rate.ToSizeRatio());
            }
        }


        public ICollection<SizeRatioDTO> SizeRates
        {
            set { _sizeRates = value; }
            get
            {
                if (_sizeRates == null)
                    InitSizeRates();
                return _sizeRates;
            }
        }
        private ICollection<SizeRatioDTO> _sizeRates;

        protected virtual void InitSizeRates()
        {
            _sizeRates = new List<SizeRatioDTO>();
        }
    }
}
