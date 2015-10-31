using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class SizeStandardDTO
    {
        partial void Initiliazing(SizeStandard entity)
        {
            Constructing(entity);
        }

        protected virtual void Constructing(SizeStandard entity)
        {
            var sizeCaptions = entity.SizeCaptions.OrderBy(c => c.DisplayOrder);
            foreach (var item in entity.SizeCaptions)
            {
                SizeCaptions.Add(new SizeCaptionDTO(item));
            }
        }

        partial void Converting(SizeStandard entity)
        {
            var sizeCaptions = SizeCaptions.OrderBy(c => c.DisplayOrder);
            foreach (var item in sizeCaptions)
            {
                entity.SizeCaptions.Add(item.ToSizeCaption());
            }
        }

        public ICollection<SizeCaptionDTO> SizeCaptions
        {
            set { _sizeCaptions = value; }
            get
            {
                if (_sizeCaptions == null)
                    _sizeCaptions = new List<SizeCaptionDTO>();
                return _sizeCaptions;
            }
        }
        private ICollection<SizeCaptionDTO> _sizeCaptions;

    }
}
