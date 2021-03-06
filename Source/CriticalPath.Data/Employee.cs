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
    
    public partial class Employee : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        public int Id { get; set; }
        public string AspNetUserId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> PositionId { get; set; }
        public Nullable<System.DateTime> InactivateDate { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual EmployeePosition Position { get; set; }
        public virtual AspNetUser CreatedUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
    	/// <summary>
    	/// Clones all properties in a new Employee instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New Employee instance</returns>
        public Employee Clone()
        {
            var clone = new Employee();
            clone.AspNetUserId = AspNetUserId;
            clone.IsActive = IsActive;
            clone.PositionId = PositionId;
            clone.InactivateDate = InactivateDate;
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
    
    	// Use below function in a partial class file (eg. Employee.part.cs)
    	// to add more complexity to clone
        partial void Cloning(Employee clone);
    }
    
    //Data Transfer Object type for Employee
    public partial class EmployeeDTO
    {
        public EmployeeDTO() { }
    
        public EmployeeDTO(Employee entity)
        {
            Id = entity.Id;
            IsActive = entity.IsActive;
            PositionId = entity.PositionId;
            InactivateDate = entity.InactivateDate;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(Employee entity);
        
        public virtual Employee ToEmployee()
        {
            var entity = new Employee();
            entity.Id = Id;
            entity.IsActive = IsActive;
            entity.PositionId = PositionId;
            entity.InactivateDate = InactivateDate;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(Employee entity);
      
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> PositionId { get; set; }
        public Nullable<System.DateTime> InactivateDate { get; set; }
    }
}
