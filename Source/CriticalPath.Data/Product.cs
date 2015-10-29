//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Product : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual ProductCategory Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    	/// <summary>
    	/// Clones all properties in a new Product instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New Product instance</returns>
        public Product Clone()
        {
            var clone = new Product();
            clone.Title = Title;
            clone.Code = Code;
            clone.Description = Description;
            clone.CategoryId = CategoryId;
            clone.ImageUrl = ImageUrl;
            clone.ModifyNr = ModifyNr;
            clone.ModifyDate = ModifyDate;
            clone.ModifierId = ModifierId;
            clone.ModifierIp = ModifierIp;
            clone.CreateDate = CreateDate;
            clone.CreatorId = CreatorId;
            clone.CreatorIp = CreatorIp;
    
            Cloning(clone);
    
            return clone;
        }
    
    	// Use below function in a partial class file (eg. Product.part.cs)
    	// to add more complexity to clone
        partial void Cloning(Product clone);
    }
    
    //Data Transfer Object type for Product
    public partial class ProductDTO
    {
        public ProductDTO() { }
    
        public ProductDTO(Product entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Code = entity.Code;
            Description = entity.Description;
            CategoryId = entity.CategoryId;
            ImageUrl = entity.ImageUrl;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(Product entity);
        
        public virtual Product ToProduct()
        {
            var entity = new Product();
            entity.Id = Id;
            entity.Title = Title;
            entity.Code = Code;
            entity.Description = Description;
            entity.CategoryId = CategoryId;
            entity.ImageUrl = ImageUrl;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(Product entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
    }
}
