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
    
    public partial class Product : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IDiscontinued, IDiscontinuedUser
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
        public int SizingStandardId { get; set; }
        public string ImageUrl { get; set; }
        public bool Discontinued { get; set; }
        public Nullable<System.DateTime> DiscontinueDate { get; set; }
        public string DiscontinueNotes { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
        public string DiscontinuedUserId { get; set; }
        public string DiscontinuedUserIp { get; set; }
    
        public virtual ProductCategory Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual SizingStandard SizingStandard { get; set; }
        public virtual AspNetUser CreatedUser { get; set; }
        public virtual AspNetUser DiscontinuedUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
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
            clone.SizingStandardId = SizingStandardId;
            clone.ImageUrl = ImageUrl;
            clone.Discontinued = Discontinued;
            clone.DiscontinueDate = DiscontinueDate;
            clone.DiscontinueNotes = DiscontinueNotes;
            clone.ModifyNr = ModifyNr;
            clone.ModifyDate = ModifyDate;
            clone.ModifierId = ModifierId;
            clone.ModifierIp = ModifierIp;
            clone.CreateDate = CreateDate;
            clone.CreatorId = CreatorId;
            clone.CreatorIp = CreatorIp;
            clone.DiscontinuedUserId = DiscontinuedUserId;
            clone.DiscontinuedUserIp = DiscontinuedUserIp;
    
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
            SizingStandardId = entity.SizingStandardId;
            ImageUrl = entity.ImageUrl;
            Discontinued = entity.Discontinued;
            DiscontinueDate = entity.DiscontinueDate;
            DiscontinueNotes = entity.DiscontinueNotes;
        
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
            entity.SizingStandardId = SizingStandardId;
            entity.ImageUrl = ImageUrl;
            entity.Discontinued = Discontinued;
            entity.DiscontinueDate = DiscontinueDate;
            entity.DiscontinueNotes = DiscontinueNotes;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(Product entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SizingStandardId { get; set; }
        public string ImageUrl { get; set; }
        public bool Discontinued { get; set; }
        public Nullable<System.DateTime> DiscontinueDate { get; set; }
        public string DiscontinueNotes { get; set; }
    }
}
