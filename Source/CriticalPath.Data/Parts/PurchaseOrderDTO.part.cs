using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class PurchaseOrderDTO : IIsApproved, IHasProduct, ICancelled, IAllPrice
    {
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
            foreach (var rate in entity.SizeRatios)
            {
                SizeRatios.Add(new SizeRatioDTO(rate));
            }
        }

        partial void Converting(PurchaseOrder entity)
        {
            foreach (var rate in SizeRatios)
            {
                entity.SizeRatios.Add(rate.ToSizeRatio());
            }
        }


        public ICollection<SizeRatioDTO> SizeRatios
        {
            set { _sizeRatios = value; }
            get
            {
                if (_sizeRatios == null)
                    InitSizeRatios();
                return _sizeRatios;
            }
        }
        private ICollection<SizeRatioDTO> _sizeRatios;

        protected virtual void InitSizeRatios()
        {
            _sizeRatios = new List<SizeRatioDTO>();
        }

    }
}
