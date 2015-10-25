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
    
    public partial class Process : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IIsApproved, IApproval
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Process()
        {
            this.ProcessSteps = new HashSet<ProcessStep>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public int ProcessTemplateId { get; set; }
        public int OrderItemId { get; set; }
        public System.DateTime TargetStartDate { get; set; }
        public Nullable<System.DateTime> TargetEndDate { get; set; }
        public Nullable<System.DateTime> ForecastStartDate { get; set; }
        public Nullable<System.DateTime> ForecastEndDate { get; set; }
        public Nullable<System.DateTime> RealizedStartDate { get; set; }
        public Nullable<System.DateTime> RealizedEndDate { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ApprovedUserId { get; set; }
        public string ApprovedUserIp { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
    
        public virtual OrderItem OrderItem { get; set; }
        public virtual ProcessTemplate ProcessTemplate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessStep> ProcessSteps { get; set; }
    	/// <summary>
    	/// Clones all properties in a new Process instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New Process instance</returns>
        public Process Clone()
        {
            var clone = new Process();
            clone.Title = Title;
            clone.IsCompleted = IsCompleted;
            clone.Description = Description;
            clone.ProcessTemplateId = ProcessTemplateId;
            clone.OrderItemId = OrderItemId;
            clone.TargetStartDate = TargetStartDate;
            clone.TargetEndDate = TargetEndDate;
            clone.ForecastStartDate = ForecastStartDate;
            clone.ForecastEndDate = ForecastEndDate;
            clone.RealizedStartDate = RealizedStartDate;
            clone.RealizedEndDate = RealizedEndDate;
            clone.IsApproved = IsApproved;
            clone.ApproveDate = ApproveDate;
            clone.ApprovedUserId = ApprovedUserId;
            clone.ApprovedUserIp = ApprovedUserIp;
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
    
    	// Use below function in a partial class file (eg. Process.part.cs)
    	// to add more complexity to clone
        partial void Cloning(Process clone);
    }
    
    //Data Transfer Object type for Process
    public partial class ProcessDTO
    {
        public ProcessDTO() { }
    
        public ProcessDTO(Process entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            IsCompleted = entity.IsCompleted;
            Description = entity.Description;
            ProcessTemplateId = entity.ProcessTemplateId;
            OrderItemId = entity.OrderItemId;
            TargetStartDate = entity.TargetStartDate;
            TargetEndDate = entity.TargetEndDate;
            ForecastStartDate = entity.ForecastStartDate;
            ForecastEndDate = entity.ForecastEndDate;
            RealizedStartDate = entity.RealizedStartDate;
            RealizedEndDate = entity.RealizedEndDate;
            IsApproved = entity.IsApproved;
            ApproveDate = entity.ApproveDate;
        
            Initilazing(entity);
        }
    
        partial void Initilazing(Process entity);
        
        public virtual Process ToProcess()
        {
            var entity = new Process();
            entity.Id = Id;
            entity.Title = Title;
            entity.IsCompleted = IsCompleted;
            entity.Description = Description;
            entity.ProcessTemplateId = ProcessTemplateId;
            entity.OrderItemId = OrderItemId;
            entity.TargetStartDate = TargetStartDate;
            entity.TargetEndDate = TargetEndDate;
            entity.ForecastStartDate = ForecastStartDate;
            entity.ForecastEndDate = ForecastEndDate;
            entity.RealizedStartDate = RealizedStartDate;
            entity.RealizedEndDate = RealizedEndDate;
            entity.IsApproved = IsApproved;
            entity.ApproveDate = ApproveDate;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(Process entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public int ProcessTemplateId { get; set; }
        public int OrderItemId { get; set; }
        public System.DateTime TargetStartDate { get; set; }
        public Nullable<System.DateTime> TargetEndDate { get; set; }
        public Nullable<System.DateTime> ForecastStartDate { get; set; }
        public Nullable<System.DateTime> ForecastEndDate { get; set; }
        public Nullable<System.DateTime> RealizedStartDate { get; set; }
        public Nullable<System.DateTime> RealizedEndDate { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
    }
}
