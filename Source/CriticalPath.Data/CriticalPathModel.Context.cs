﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CriticalPathContext : AbstractCriticalPathContext
    {
        
    }
    
    public abstract partial class AbstractCriticalPathContext : DbContext
    {
        public AbstractCriticalPathContext()
            : base("name=CriticalPathContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessStep> ProcessSteps { get; set; }
        public virtual DbSet<ProcessStepTemplate> ProcessStepTemplates { get; set; }
        public virtual DbSet<ProcessTemplate> ProcessTemplates { get; set; }
        public virtual DbSet<SizeRate> SizeRates { get; set; }
        public virtual DbSet<SizingStandard> SizingStandards { get; set; }
        public virtual DbSet<Sizing> Sizings { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
    
        /// <summary>
        /// Default query for AspNetUsers
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<AspNetUser> GetAspNetUserQuery()
        {
            IQueryable<AspNetUser> query = AspNetUsers.OrderBy(p => p.LastName);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from AspNetUser query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<AspNetUserDTO> GetAspNetUserDtoQuery()
        {
            return GetAspNetUserDtoQuery(GetAspNetUserQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from AspNetUser query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<AspNetUserDTO> GetAspNetUserDtoQuery(IQueryable<AspNetUser> query)
        {
            return from e in query
                   select new AspNetUserDTO
                   {
                       Id = e.Id,
                       FirstName = e.FirstName,
                       LastName = e.LastName,
                       Email = e.Email,
                       EmailConfirmed = e.EmailConfirmed,
                       PasswordHash = e.PasswordHash,
                       SecurityStamp = e.SecurityStamp,
                       PhoneNumber = e.PhoneNumber,
                       PhoneNumberConfirmed = e.PhoneNumberConfirmed,
                       TwoFactorEnabled = e.TwoFactorEnabled,
                       LockoutEndDateUtc = e.LockoutEndDateUtc,
                       LockoutEnabled = e.LockoutEnabled,
                       AccessFailedCount = e.AccessFailedCount,
                       UserName = e.UserName,
                   };
        }
    
    
        /// <summary>
        /// Default query for Companies
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Customer> GetCustomerQuery()
        {
            IQueryable<Customer> query = Companies.OrderBy(p => p.CompanyName)
    														.OfType<Customer>();
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Customer query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<CustomerDTO> GetCustomerDtoQuery()
        {
            return GetCustomerDtoQuery(GetCustomerQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Customer query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<CustomerDTO> GetCustomerDtoQuery(IQueryable<Customer> query)
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
                       City = e.City,
                       State = e.State,
                       ZipCode = e.ZipCode,
                       Country = e.Country,
                       Discontinued = e.Discontinued,
                       DiscontinueDate = e.DiscontinueDate,
                       DiscontinueNotes = e.DiscontinueNotes,
                       Notes = e.Notes,
                       CustomerCode = e.CustomerCode,
                   };
        }
    
    
        /// <summary>
        /// Default query for Companies
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Supplier> GetSupplierQuery()
        {
            IQueryable<Supplier> query = Companies.OrderBy(p => p.CompanyName)
    														.OfType<Supplier>();
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Supplier query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SupplierDTO> GetSupplierDtoQuery()
        {
            return GetSupplierDtoQuery(GetSupplierQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Supplier query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SupplierDTO> GetSupplierDtoQuery(IQueryable<Supplier> query)
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
                       City = e.City,
                       State = e.State,
                       ZipCode = e.ZipCode,
                       Country = e.Country,
                       Discontinued = e.Discontinued,
                       DiscontinueDate = e.DiscontinueDate,
                       DiscontinueNotes = e.DiscontinueNotes,
                       Notes = e.Notes,
                       SupplierCode = e.SupplierCode,
                   };
        }
    
    
        /// <summary>
        /// Default query for Contacts
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Contact> GetContactQuery()
        {
            IQueryable<Contact> query = Contacts.OrderBy(p => p.LastName);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Contact query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ContactDTO> GetContactDtoQuery()
        {
            return GetContactDtoQuery(GetContactQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Contact query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ContactDTO> GetContactDtoQuery(IQueryable<Contact> query)
        {
            return from e in query
                   select new ContactDTO
                   {
                       Id = e.Id,
                       CompanyId = e.CompanyId,
                       FirstName = e.FirstName,
                       LastName = e.LastName,
                       EmailWork = e.EmailWork,
                       EmailHome = e.EmailHome,
                       PhoneMobile = e.PhoneMobile,
                       PhoneWork1 = e.PhoneWork1,
                       PhoneWork2 = e.PhoneWork2,
                       Discontinued = e.Discontinued,
                       DiscontinueDate = e.DiscontinueDate,
                       DiscontinueNotes = e.DiscontinueNotes,
                       Notes = e.Notes,
                   };
        }
    
    
        /// <summary>
        /// Default query for Processes
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Process> GetProcessQuery()
        {
            IQueryable<Process> query = Processes;
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Process query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessDTO> GetProcessDtoQuery()
        {
            return GetProcessDtoQuery(GetProcessQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Process query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessDTO> GetProcessDtoQuery(IQueryable<Process> query)
        {
            return from e in query
                   select new ProcessDTO
                   {
                       Id = e.Id,
                       IsCompleted = e.IsCompleted,
                       Description = e.Description,
                       ProcessTemplateId = e.ProcessTemplateId,
                       PurchaseOrderId = e.PurchaseOrderId,
                       SupplierId = e.SupplierId,
                       StartDate = e.StartDate,
                       TargetDate = e.TargetDate,
                       ForecastDate = e.ForecastDate,
                       RealizedDate = e.RealizedDate,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
                       Cancelled = e.Cancelled,
                       CancelDate = e.CancelDate,
                       CancelNotes = e.CancelNotes,
                   };
        }
    
    
        /// <summary>
        /// Default query for ProcessSteps
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<ProcessStep> GetProcessStepQuery()
        {
            IQueryable<ProcessStep> query = ProcessSteps.OrderBy(p => p.DisplayOrder);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProcessStep query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessStepDTO> GetProcessStepDtoQuery()
        {
            return GetProcessStepDtoQuery(GetProcessStepQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProcessStep query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessStepDTO> GetProcessStepDtoQuery(IQueryable<ProcessStep> query)
        {
            return from e in query
                   select new ProcessStepDTO
                   {
                       Id = e.Id,
                       Title = e.Title,
                       IsCompleted = e.IsCompleted,
                       Description = e.Description,
                       DisplayOrder = e.DisplayOrder,
                       ProcessId = e.ProcessId,
                       TemplateId = e.TemplateId,
                       TargetDate = e.TargetDate,
                       ForecastDate = e.ForecastDate,
                       RealizedDate = e.RealizedDate,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
                   };
        }
    
    
        /// <summary>
        /// Default query for ProcessStepTemplates
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<ProcessStepTemplate> GetProcessStepTemplateQuery()
        {
            IQueryable<ProcessStepTemplate> query = ProcessStepTemplates.OrderBy(p => p.DisplayOrder);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProcessStepTemplate query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessStepTemplateDTO> GetProcessStepTemplateDtoQuery()
        {
            return GetProcessStepTemplateDtoQuery(GetProcessStepTemplateQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProcessStepTemplate query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessStepTemplateDTO> GetProcessStepTemplateDtoQuery(IQueryable<ProcessStepTemplate> query)
        {
            return from e in query
                   select new ProcessStepTemplateDTO
                   {
                       Id = e.Id,
                       Title = e.Title,
                       DisplayOrder = e.DisplayOrder,
                       ProcessTemplateId = e.ProcessTemplateId,
                       RequiredWorkDays = e.RequiredWorkDays,
                   };
        }
    
    
        /// <summary>
        /// Default query for ProcessTemplates
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<ProcessTemplate> GetProcessTemplateQuery()
        {
            IQueryable<ProcessTemplate> query = ProcessTemplates.OrderBy(p => p.TemplateName);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProcessTemplate query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessTemplateDTO> GetProcessTemplateDtoQuery()
        {
            return GetProcessTemplateDtoQuery(GetProcessTemplateQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProcessTemplate query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProcessTemplateDTO> GetProcessTemplateDtoQuery(IQueryable<ProcessTemplate> query)
        {
            return from e in query
                   select new ProcessTemplateDTO
                   {
                       Id = e.Id,
                       TemplateName = e.TemplateName,
                       DefaultTitle = e.DefaultTitle,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
                   };
        }
    
    
        /// <summary>
        /// Default query for ProductCategories
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<ProductCategory> GetProductCategoryQuery()
        {
            IQueryable<ProductCategory> query = ProductCategories.OrderBy(p => p.Title);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProductCategory query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProductCategoryDTO> GetProductCategoryDtoQuery()
        {
            return GetProductCategoryDtoQuery(GetProductCategoryQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from ProductCategory query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProductCategoryDTO> GetProductCategoryDtoQuery(IQueryable<ProductCategory> query)
        {
            return from e in query
                   select new ProductCategoryDTO
                   {
                       Id = e.Id,
                       Title = e.Title,
                       Code = e.Code,
                       Description = e.Description,
                       ParentCategoryId = e.ParentCategoryId,
                   };
        }
    
    
        /// <summary>
        /// Default query for Products
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Product> GetProductQuery()
        {
            IQueryable<Product> query = Products.OrderBy(p => p.Title);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Product query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProductDTO> GetProductDtoQuery()
        {
            return GetProductDtoQuery(GetProductQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Product query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<ProductDTO> GetProductDtoQuery(IQueryable<Product> query)
        {
            return from e in query
                   select new ProductDTO
                   {
                       Id = e.Id,
                       Title = e.Title,
                       Code = e.Code,
                       Description = e.Description,
                       CategoryId = e.CategoryId,
                       SizingStandardId = e.SizingStandardId,
                       ImageUrl = e.ImageUrl,
                       Discontinued = e.Discontinued,
                       DiscontinueDate = e.DiscontinueDate,
                       DiscontinueNotes = e.DiscontinueNotes,
                   };
        }
    
    
        /// <summary>
        /// Default query for PurchaseOrders
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<PurchaseOrder> GetPurchaseOrderQuery()
        {
            IQueryable<PurchaseOrder> query = PurchaseOrders;
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from PurchaseOrder query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<PurchaseOrderDTO> GetPurchaseOrderDtoQuery()
        {
            return GetPurchaseOrderDtoQuery(GetPurchaseOrderQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from PurchaseOrder query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<PurchaseOrderDTO> GetPurchaseOrderDtoQuery(IQueryable<PurchaseOrder> query)
        {
            return from e in query
                   select new PurchaseOrderDTO
                   {
                       Id = e.Id,
                       CustomerId = e.CustomerId,
                       ProductId = e.ProductId,
                       SizingStandardId = e.SizingStandardId,
                       OrderDate = e.OrderDate,
                       DueDate = e.DueDate,
                       Code = e.Code,
                       Description = e.Description,
                       Quantity = e.Quantity,
                       UnitPrice = e.UnitPrice,
                       SizeRateDivisor = e.SizeRateDivisor,
                       Notes = e.Notes,
                       ApproveDate = e.ApproveDate,
                       Cancelled = e.Cancelled,
                       CancelDate = e.CancelDate,
                       CancelNotes = e.CancelNotes,
                       IsApproved = e.IsApproved,
                   };
        }
    
    
        /// <summary>
        /// Default query for SizeRates
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<SizeRate> GetSizeRateQuery()
        {
            IQueryable<SizeRate> query = SizeRates.OrderBy(p => p.DisplayOrder);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from SizeRate query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SizeRateDTO> GetSizeRateDtoQuery()
        {
            return GetSizeRateDtoQuery(GetSizeRateQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from SizeRate query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SizeRateDTO> GetSizeRateDtoQuery(IQueryable<SizeRate> query)
        {
            return from e in query
                   select new SizeRateDTO
                   {
                       Id = e.Id,
                       DisplayOrder = e.DisplayOrder,
                       Caption = e.Caption,
                       Rate = e.Rate,
                       PurchaseOrderId = e.PurchaseOrderId,
                   };
        }
    
    
        /// <summary>
        /// Default query for Sizings
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Sizing> GetSizingQuery()
        {
            IQueryable<Sizing> query = Sizings.OrderBy(p => p.DisplayOrder);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Sizing query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SizingDTO> GetSizingDtoQuery()
        {
            return GetSizingDtoQuery(GetSizingQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from Sizing query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SizingDTO> GetSizingDtoQuery(IQueryable<Sizing> query)
        {
            return from e in query
                   select new SizingDTO
                   {
                       Id = e.Id,
                       DisplayOrder = e.DisplayOrder,
                       Caption = e.Caption,
                       SizingStandardId = e.SizingStandardId,
                   };
        }
    
    
        /// <summary>
        /// Default query for SizingStandards
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<SizingStandard> GetSizingStandardQuery()
        {
            IQueryable<SizingStandard> query = SizingStandards.OrderBy(p => p.Title);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from SizingStandard query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SizingStandardDTO> GetSizingStandardDtoQuery()
        {
            return GetSizingStandardDtoQuery(GetSizingStandardQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from SizingStandard query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<SizingStandardDTO> GetSizingStandardDtoQuery(IQueryable<SizingStandard> query)
        {
            return from e in query
                   select new SizingStandardDTO
                   {
                       Id = e.Id,
                       Title = e.Title,
                   };
        }
    
    }
}
