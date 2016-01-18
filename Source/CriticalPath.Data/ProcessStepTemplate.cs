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
    
    public partial class ProcessStepTemplate : IDisplayOrder, ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcessStepTemplate()
        {
            this.ProcessSteps = new HashSet<ProcessStep>();
            this.AfterSteps = new HashSet<ProcessStepTemplate>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public int ProcessTemplateId { get; set; }
        public int RequiredWorkDays { get; set; }
        public bool IgnoreInRepeat { get; set; }
        public int ModifyNr { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifierIp { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorIp { get; set; }
        public int ReqDaysBeforeDueDate { get; set; }
        public Nullable<int> DependedStepId { get; set; }
    
        public virtual ProcessTemplate ProcessTemplate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessStep> ProcessSteps { get; set; }
        public virtual AspNetUser CreatedUser { get; set; }
        public virtual AspNetUser ModifiedUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessStepTemplate> AfterSteps { get; set; }
        public virtual ProcessStepTemplate DependedStep { get; set; }
    	/// <summary>
    	/// Clones all properties in a new ProcessStepTemplate instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New ProcessStepTemplate instance</returns>
        public ProcessStepTemplate Clone()
        {
            var clone = new ProcessStepTemplate();
            clone.Title = Title;
            clone.DisplayOrder = DisplayOrder;
            clone.ProcessTemplateId = ProcessTemplateId;
            clone.RequiredWorkDays = RequiredWorkDays;
            clone.IgnoreInRepeat = IgnoreInRepeat;
            clone.ModifyNr = ModifyNr;
            clone.ModifyDate = ModifyDate;
            clone.ModifierId = ModifierId;
            clone.ModifierIp = ModifierIp;
            clone.CreateDate = CreateDate;
            clone.CreatorId = CreatorId;
            clone.CreatorIp = CreatorIp;
            clone.ReqDaysBeforeDueDate = ReqDaysBeforeDueDate;
            clone.DependedStepId = DependedStepId;
    
            Cloning(clone);
    
            return clone;
        }
    
    	// Use below function in a partial class file (eg. ProcessStepTemplate.part.cs)
    	// to add more complexity to clone
        partial void Cloning(ProcessStepTemplate clone);
    }
    
    //Data Transfer Object type for ProcessStepTemplate
    public partial class ProcessStepTemplateDTO
    {
        public ProcessStepTemplateDTO() { }
    
        public ProcessStepTemplateDTO(ProcessStepTemplate entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            DisplayOrder = entity.DisplayOrder;
            ProcessTemplateId = entity.ProcessTemplateId;
            RequiredWorkDays = entity.RequiredWorkDays;
            IgnoreInRepeat = entity.IgnoreInRepeat;
            ReqDaysBeforeDueDate = entity.ReqDaysBeforeDueDate;
            DependedStepId = entity.DependedStepId;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(ProcessStepTemplate entity);
        
        public virtual ProcessStepTemplate ToProcessStepTemplate()
        {
            var entity = new ProcessStepTemplate();
            entity.Id = Id;
            entity.Title = Title;
            entity.DisplayOrder = DisplayOrder;
            entity.ProcessTemplateId = ProcessTemplateId;
            entity.RequiredWorkDays = RequiredWorkDays;
            entity.IgnoreInRepeat = IgnoreInRepeat;
            entity.ReqDaysBeforeDueDate = ReqDaysBeforeDueDate;
            entity.DependedStepId = DependedStepId;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(ProcessStepTemplate entity);
      
        public int Id { get; set; }
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public int ProcessTemplateId { get; set; }
        public int RequiredWorkDays { get; set; }
        public bool IgnoreInRepeat { get; set; }
        public int ReqDaysBeforeDueDate { get; set; }
        public Nullable<int> DependedStepId { get; set; }
    }
}
