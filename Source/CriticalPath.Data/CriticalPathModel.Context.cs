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
        public virtual DbSet<PuchaseOrder> PuchaseOrders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<ProcessStep> ProcessSteps { get; set; }
        public virtual DbSet<ProcessStepTemplate> ProcessStepTemplates { get; set; }
        public virtual DbSet<ProcessTemplate> ProcessTemplates { get; set; }
    
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
                       FirstName = e.FirstName,
                       LastName = e.LastName,
                       EmailWork = e.EmailWork,
                       EmailHome = e.EmailHome,
                       PhoneMobile = e.PhoneMobile,
                       PhoneWork1 = e.PhoneWork1,
                       PhoneWork2 = e.PhoneWork2,
                       Notes = e.Notes,
                       CompanyId = e.CompanyId,
                   };
        }
    
    
        /// <summary>
        /// Default query for OrderItems
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<OrderItem> GetOrderItemQuery()
        {
            IQueryable<OrderItem> query = OrderItems.OrderBy(p => p.DisplayOrder);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from OrderItem query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<OrderItemDTO> GetOrderItemDtoQuery()
        {
            return GetOrderItemDtoQuery(GetOrderItemQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from OrderItem query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<OrderItemDTO> GetOrderItemDtoQuery(IQueryable<OrderItem> query)
        {
            return from e in query
                   select new OrderItemDTO
                   {
                       Id = e.Id,
                       PuchaseOrderId = e.PuchaseOrderId,
                       ProductId = e.ProductId,
                       DisplayOrder = e.DisplayOrder,
                       Quantity = e.Quantity,
                       Notes = e.Notes,
                   };
        }
    
    
        /// <summary>
        /// Default query for Processes
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Process> GetProcessQuery()
        {
            IQueryable<Process> query = Processes.OrderBy(p => p.Title);
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
                       Title = e.Title,
                       IsCompleted = e.IsCompleted,
                       Description = e.Description,
                       ProcessTemplateId = e.ProcessTemplateId,
                       OrderItemId = e.OrderItemId,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
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
                   };
        }
    
    
        /// <summary>
        /// Default query for PuchaseOrders
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<PuchaseOrder> GetPuchaseOrderQuery()
        {
            IQueryable<PuchaseOrder> query = PuchaseOrders.OrderBy(p => p.Title);
            return query;
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from PuchaseOrder query
        /// </summary>
        /// <param name="query">query to be converted</param>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<PuchaseOrderDTO> GetPuchaseOrderDtoQuery()
        {
            return GetPuchaseOrderDtoQuery(GetPuchaseOrderQuery());
        }
    
        /// <summary>
        /// Gets a lighter data transfer object query from PuchaseOrder query
        /// </summary>
        /// <returns>Converted data transfer object query</returns>
        public virtual IQueryable<PuchaseOrderDTO> GetPuchaseOrderDtoQuery(IQueryable<PuchaseOrder> query)
        {
            return from e in query
                   select new PuchaseOrderDTO
                   {
                       Id = e.Id,
                       Title = e.Title,
                       CustomerId = e.CustomerId,
                       OrderDate = e.OrderDate,
                       DueDate = e.DueDate,
                       Code = e.Code,
                       Description = e.Description,
                       Notes = e.Notes,
                       IsApproved = e.IsApproved,
                       ApproveDate = e.ApproveDate,
                   };
        }
    
    }
}
