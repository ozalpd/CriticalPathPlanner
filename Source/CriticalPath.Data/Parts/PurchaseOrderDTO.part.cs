using CP.i8n;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            if (entity.Designer?.AspNetUser != null)
                DesignerName = string.Format("{0} {1}", entity.Designer.AspNetUser.FirstName, entity.Designer.AspNetUser.LastName);

            if (entity.Merchandiser1?.AspNetUser != null)
                Merchandiser1Name = string.Format("{0} {1}", entity.Merchandiser1.AspNetUser.FirstName, entity.Merchandiser1.AspNetUser.LastName);

            if (entity.Merchandiser2?.AspNetUser != null)
                Merchandiser2Name = string.Format("{0} {1}", entity.Merchandiser2.AspNetUser.FirstName, entity.Merchandiser2.AspNetUser.LastName);
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



        [Display(ResourceType = typeof(EntityStrings), Name = "Designer")]
        public string DesignerName { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Merchandiser1")]
        public string Merchandiser1Name { get; set; }

        [Display(ResourceType = typeof(EntityStrings), Name = "Merchandiser2")]
        public string Merchandiser2Name { get; set; }
    }
}
