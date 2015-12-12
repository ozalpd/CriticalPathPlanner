using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class ProductDTO : IDiscontinued, IRetailPrice, IRoyaltyFee, IBuyingPrice, ISellingPrice, IAllPrice
    {
        partial void Initiliazing(Product entity)
        {
            Constructing(entity);
        }

        protected virtual void Constructing(Product entity) { }
    }
}
