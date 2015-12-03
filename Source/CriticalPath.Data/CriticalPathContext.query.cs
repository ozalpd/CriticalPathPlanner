using System.Data.Entity;
using System.Linq;
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

        public override IQueryable<Country> GetCountryQuery(bool publishedOnly = true)
        {
            return base.GetCountryQuery(publishedOnly)
                    .OrderBy(c => c.DisplayOrder)
                    .ThenBy(c => c.CountryName);
        }

        public override IQueryable<CountryDTO> GetCountryDtoQuery(IQueryable<Country> query)
        {
            return from e in query
                   select new CountryDTO
                   {
                       Id = e.Id,
                       CountryName = e.CountryName,
                       TwoLetterIsoCode = e.TwoLetterIsoCode,
                       ThreeLetterIsoCode = e.ThreeLetterIsoCode,
                       NumericIsoCode = e.NumericIsoCode,
                       DisplayOrder = e.DisplayOrder,
                       IsPublished = e.IsPublished
                   };
        }

        public override IQueryable<FreightTerm> GetFreightTermQuery(bool publishedOnly = true)
        {
            return base.GetFreightTermQuery(publishedOnly)
                    .OrderByDescending(ft=>ft.IsPublished)
                    .ThenBy(ft => ft.IncotermCode);
        }

        public override IQueryable<ProductCategory> GetProductCategoryQuery()
        {
            return base.GetProductCategoryQuery()
                    .OrderBy(pc => pc.ParentCategory.CategoryName)
                    .ThenBy(pc => pc.CategoryName);
        }

        public override IQueryable<Product> GetProductQuery()
        {
            return base.GetProductQuery().OrderBy(p => p.ProductCode);
        }

        public override IQueryable<PurchaseOrder> GetPurchaseOrderQuery()
        {
            return base.GetPurchaseOrderQuery()
                    .OrderByDescending(po => po.OrderDate)
                    .ThenByDescending(po => po.ApproveDate);
        }

        public async Task<ProductCategory> FindProductCategory(int id)
        {
            return await GetProductCategoryQuery().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
