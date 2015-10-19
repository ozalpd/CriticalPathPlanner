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
    
    public partial class ProcessStep : IDisplayOrder, ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IApproval
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public int ProcessId { get; set; }
        public Nullable<System.DateTime> TargetDate { get; set; }
        public Nullable<System.DateTime> ForecastDate { get; set; }
        public Nullable<System.DateTime> RealizedDate { get; set; }
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
    
        public virtual Process Process { get; set; }
    	/// <summary>
    	/// Clones all properties in a new ProcessStep instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New ProcessStep instance</returns>
        public ProcessStep Clone()
        {
            var clone = new ProcessStep();
            clone.Title = Title;
            clone.IsCompleted = IsCompleted;
            clone.Description = Description;
            clone.DisplayOrder = DisplayOrder;
            clone.ProcessId = ProcessId;
            clone.TargetDate = TargetDate;
            clone.ForecastDate = ForecastDate;
            clone.RealizedDate = RealizedDate;
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
    
    	// Use below function in a partial class file (eg. ProcessStep.part.cs)
    	// to add more complexity to clone
        partial void Cloning(ProcessStep clone);
    }
    
    //Data Transfer Object type for ProcessStep
    public partial class ProcessStepDTO
    {
        public ProcessStepDTO() { }
    
        public ProcessStepDTO(ProcessStep entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            IsCompleted = entity.IsCompleted;
            Description = entity.Description;
            DisplayOrder = entity.DisplayOrder;
            ProcessId = entity.ProcessId;
            TargetDate = entity.TargetDate;
            ForecastDate = entity.ForecastDate;
            RealizedDate = entity.RealizedDate;
            IsApproved = entity.IsApproved;
            ApproveDate = entity.ApproveDate;
        
            Initilazing(entity);
        }
    
        partial void Initilazing(ProcessStep entity);
        
        public virtual ProcessStep ToProcessStep()
        {
            var entity = new ProcessStep();
            entity.Id = Id;
            entity.Title = Title;
            entity.IsCompleted = IsCompleted;
            entity.Description = Description;
            entity.DisplayOrder = DisplayOrder;
            entity.ProcessId = ProcessId;
            entity.TargetDate = TargetDate;
            entity.ForecastDate = ForecastDate;
            entity.RealizedDate = RealizedDate;
            entity.IsApproved = IsApproved;
            entity.ApproveDate = ApproveDate;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(ProcessStep entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public int ProcessId { get; set; }
        public Nullable<System.DateTime> TargetDate { get; set; }
        public Nullable<System.DateTime> ForecastDate { get; set; }
        public Nullable<System.DateTime> RealizedDate { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
    }
}
