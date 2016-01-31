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

        public override IQueryable<ProcessStepRevision> GetProcessStepRevisionQuery()
        {
            return base.GetProcessStepRevisionQuery().OrderBy(sr => sr.Id);
        }

        public override IQueryable<CustomerDepartment> GetCustomerDepartmentQuery()
        {
            IQueryable<CustomerDepartment> query = CustomerDepartments
                                    .OrderBy(d => d.CustomerId)
                                    .ThenBy(d => d.DepartmentName)
                                    .ThenBy(d => d.Id);
            return query;
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

        public override IQueryable<Employee> GetEmployeeQuery()
        {
            return base.GetEmployeeQuery()
                    .OrderBy(e => e.AspNetUser.FirstName)
                    .ThenBy(e => e.AspNetUser.LastName);
        }

        public override IQueryable<EmployeeDTO> GetEmployeeDtoQuery(IQueryable<Employee> query)
        {
            return from e in query
                   select new EmployeeDTO
                   {
                       Id = e.Id,
                       IsActive = e.IsActive,
                       InactivateDate = e.InactivateDate,
                       UserName = e.AspNetUser.UserName,
                       FirstName = e.AspNetUser.FirstName,
                       LastName = e.AspNetUser.LastName,
                       Position = e.Position.Position,
                       PhoneNumber = e.AspNetUser.PhoneNumber,
                       Email = e.AspNetUser.Email
                   };
        }

        public override IQueryable<EmployeePosition> GetEmployeePositionQuery()
        {
            return base.GetEmployeePositionQuery().OrderByDescending(p => p.AppDefault).ThenBy(p => p.Position);
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

        public override IQueryable<PurchaseOrderDTO> GetPurchaseOrderDtoQuery(IQueryable<PurchaseOrder> query)
        {
            return from e in query
                   select new PurchaseOrderDTO
                   {
                       Id = e.Id,
                       PoNr = e.PoNr,
                       RefCode = e.RefCode,
                       KimballNr = e.KimballNr,
                       OrderDate = e.OrderDate,
                       DueDate = e.DueDate,
                       IsRepeat = e.IsRepeat,
                       ParentPoId = e.ParentPoId,
                       DesignerId = e.DesignerId,
                       Merchandiser1Id = e.Merchandiser1Id,
                       Merchandiser2Id = e.Merchandiser2Id,
                       Description = e.Description,
                       CustomerId = e.CustomerId,
                       CustomerDepartmentId = e.CustomerDepartmentId,
                       CustomerPoNr = e.CustomerPoNr,
                       LicensorId = e.LicensorId,
                       ProductId = e.ProductId,
                       SizingStandardId = e.SizingStandardId,
                       FabricComposition = e.FabricComposition,
                       Colour = e.Colour,
                       Print = e.Print,
                       ShipmentHangingFolded = e.ShipmentHangingFolded,
                       HangerSticker = e.HangerSticker,
                       Labelling = e.Labelling,
                       WoovenLabel = e.WoovenLabel,
                       WashingInstructions = e.WashingInstructions,
                       Quantity = e.Quantity,
                       DiscountRate = e.DiscountRate,
                       UnitPrice = e.UnitPrice,
                       SellingCurrencyId = e.SellingCurrencyId,
                       UnitPrice2 = e.UnitPrice2,
                       SellingCurrency2Id = e.SellingCurrency2Id,
                       LicensorPrice = e.LicensorPrice,
                       LicensorCurrencyId = e.LicensorCurrencyId,
                       BuyingPrice = e.BuyingPrice,
                       BuyingCurrencyId = e.BuyingCurrencyId,
                       BuyingPrice2 = e.BuyingPrice2,
                       BuyingCurrency2Id = e.BuyingCurrency2Id,
                       RoyaltyFee = e.RoyaltyFee,
                       RoyaltyCurrencyId = e.RoyaltyCurrencyId,
                       RetailPrice = e.RetailPrice,
                       RetailCurrencyId = e.RetailCurrencyId,
                       SizeRatioDivisor = e.SizeRatioDivisor,
                       FreightTermId = e.FreightTermId,
                       SupplierId = e.SupplierId,
                       SupplierDueDate = e.SupplierDueDate,
                       Notes = e.Notes,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
                       Cancelled = e.Cancelled,
                       CancelDate = e.CancelDate,
                       CancellationReason = e.CancellationReason,
                       DesignerName = e.Designer.AspNetUser.FirstName + " " + e.Designer.AspNetUser.LastName,
                       Merchandiser1Name = e.Merchandiser1.AspNetUser.FirstName + " " + e.Merchandiser1.AspNetUser.LastName,
                       Merchandiser2Name = e.Merchandiser2.AspNetUser.FirstName + " " + e.Merchandiser2.AspNetUser.LastName
                   };
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
