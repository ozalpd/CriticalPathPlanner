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

        public override IQueryable<CustomerDTO> GetCustomerDtoQuery(IQueryable<Customer> query)
        {
            return from e in query
                   select new CustomerDTO
                   {
                       Id = e.Id,
                       CompanyName = e.CompanyName,
                       Phone1 = e.Phone1,
                       Phone2 = e.Phone2,
                       Phone3 = e.Phone3,
                       Address1 = e.Address1,
                       Address2 = e.Address2,
                       ZipCode = e.ZipCode,
                       City = e.City,
                       State = e.State,
                       CountryId = e.CountryId,
                       Discontinued = e.Discontinued,
                       DiscontinueDate = e.DiscontinueDate,
                       DiscontinueNotes = e.DiscontinueNotes,
                       Notes = e.Notes,
                       CustomerCode = e.CustomerCode,
                       DiscountRate = e.DiscountRate,
                       Country = e.Country.CountryName
                   };
        }

        public override IQueryable<SupplierDTO> GetSupplierDtoQuery(IQueryable<Supplier> query)
        {
            return from e in query
                   select new SupplierDTO
                   {
                       Id = e.Id,
                       CompanyName = e.CompanyName,
                       Phone1 = e.Phone1,
                       Phone2 = e.Phone2,
                       Phone3 = e.Phone3,
                       Address1 = e.Address1,
                       Address2 = e.Address2,
                       ZipCode = e.ZipCode,
                       City = e.City,
                       State = e.State,
                       CountryId = e.CountryId,
                       Discontinued = e.Discontinued,
                       DiscontinueDate = e.DiscontinueDate,
                       DiscontinueNotes = e.DiscontinueNotes,
                       Notes = e.Notes,
                       SupplierCode = e.SupplierCode,
                       Country = e.Country.CountryName
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
                    .ThenByDescending(po => po.ApproveDate)
                    .ThenBy(po => po.PoNr)
                    .ThenByDescending(po => po.Id);
        }

        public override IQueryable<Process> GetProcessQuery()
        {
            return base.GetProcessQuery()
                        .OrderByDescending(p => p.PurchaseOrder.OrderDate)
                        .ThenByDescending(p => p.PurchaseOrder.ApproveDate)
                        .ThenByDescending(p => p.ApproveDate);
        }

        public override IQueryable<ProcessDTO> GetProcessDtoQuery(IQueryable<Process> query)
        {
            return from e in query
                   select new ProcessDTO
                   {
                       Id = e.Id,
                       IsCompleted = e.IsCompleted,
                       Description = e.Description,
                       ProcessTemplateId = e.ProcessTemplateId,
                       PurchaseOrderId = e.PurchaseOrderId,
                       CurrentStepId = e.CurrentStepId,
                       StartDate = e.StartDate,
                       TargetDate = e.TargetDate,
                       ForecastDate = e.ForecastDate,
                       RealizedDate = e.RealizedDate,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
                       Cancelled = e.Cancelled,
                       CancelDate = e.CancelDate,
                       CancellationReason = e.CancellationReason,
                       PoNr = e.PurchaseOrder.PoNr,
                       CustomerName = e.PurchaseOrder.Customer.CompanyName,
                       SupplierName = e.PurchaseOrder.Supplier.CompanyName,
                       DueDate = e.PurchaseOrder.DueDate,
                       IsRepeat = e.PurchaseOrder.IsRepeat,
                       OrderDate = e.PurchaseOrder.OrderDate,
                       Quantity = e.PurchaseOrder.Quantity,
                       ProductCode = e.PurchaseOrder.Product.ProductCode,
                       ProductDescription = e.PurchaseOrder.Product.Description,
                       ImageUrl = e.PurchaseOrder.Product.ImageUrl,
                       CategoryName = e.PurchaseOrder.Product.Category.CategoryName,
                       ParentCategoryName = e.PurchaseOrder.Product.Category.ParentCategory.CategoryName
                   };
        }
        public async Task<ProductCategory> FindProductCategory(int id)
        {
            return await GetProductCategoryQuery().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
