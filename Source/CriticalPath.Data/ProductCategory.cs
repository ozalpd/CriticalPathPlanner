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
    
    public partial class ProductCategory : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            this.SubCategories = new HashSet<ProductCategory>();
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCategory> SubCategories { get; set; }
        public virtual ProductCategory ParentCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    	/// <summary>
    	/// Clones all properties in a new ProductCategory instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New ProductCategory instance</returns>
        public ProductCategory Clone()
        {
            var clone = new ProductCategory();
            clone.Title = Title;
            clone.Code = Code;
            clone.Description = Description;
            clone.ParentCategoryId = ParentCategoryId;
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
    
    	// Use below function in a partial class file (eg. ProductCategory.part.cs)
    	// to add more complexity to clone
        partial void Cloning(ProductCategory clone);
    }
    
    //Data Transfer Object type for ProductCategory
    public partial class ProductCategoryDTO
    {
        public ProductCategoryDTO() { }
    
        public ProductCategoryDTO(ProductCategory entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Code = entity.Code;
            Description = entity.Description;
            ParentCategoryId = entity.ParentCategoryId;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(ProductCategory entity);
        
        public virtual ProductCategory ToProductCategory()
        {
            var entity = new ProductCategory();
            entity.Id = Id;
            entity.Title = Title;
            entity.Code = Code;
            entity.Description = Description;
            entity.ParentCategoryId = ParentCategoryId;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(ProductCategory entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
    }
}
