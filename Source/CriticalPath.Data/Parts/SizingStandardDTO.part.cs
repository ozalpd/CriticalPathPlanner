using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class SizingStandardDTO
    {
        partial void Initiliazing(SizingStandard entity)
        {
            Constructing(entity);
        }

        protected virtual void Constructing(SizingStandard entity)
        {
            var sizings = entity.Sizings.OrderBy(c => c.DisplayOrder);
            foreach (var item in entity.Sizings)
            {
                Sizings.Add(new SizingDTO(item));
            }
        }

        partial void Converting(SizingStandard entity)
        {
            var sizings = Sizings.OrderBy(c => c.DisplayOrder);
            foreach (var item in sizings)
            {
                entity.Sizings.Add(item.ToSizing());
            }
        }

        public ICollection<SizingDTO> Sizings
        {
            set { _sizings = value; }
            get
            {
                if (_sizings == null)
                    _sizings = new List<SizingDTO>();
                return _sizings;
            }
        }
        private ICollection<SizingDTO> _sizings;

    }
}
