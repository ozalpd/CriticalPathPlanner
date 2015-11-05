using OzzIdentity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CriticalPathContext
    {
        public override IQueryable<Contact> GetContactQuery()
        {
            return base.GetContactQuery()
                    .OrderBy(c => c.FirstName)
                    .ThenBy(c => c.LastName);
        }

        public async Task<ProductCategory> FindProductCategory(int id)
        {
            return await GetProductCategoryQuery().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<SizingStandardDTO>> GetSizingStandardDtoList()
        {
            if (_sizingStandardDtos == null)
            {
                var list = await GetSizingStandardQuery().ToListAsync();
                _sizingStandardDtos = new List<SizingStandardDTO>();
                foreach (var item in list)
                {
                    _sizingStandardDtos.Add(new SizingStandardDTO(item));
                }
            }

            return _sizingStandardDtos;
        }
        static List<SizingStandardDTO> _sizingStandardDtos;
    }
}
