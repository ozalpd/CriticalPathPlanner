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
    
    public partial class ProcessTemplate : ICreatorId, ICreatorIp, ICreateDate, IModifyNr, IModifierId, IModifierIp, IModifyDate, IIsApproved, IApproval
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcessTemplate()
        {
            this.Processes = new HashSet<Process>();
            this.StepTemplates = new HashSet<ProcessStepTemplate>();
        }
    
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string DefaultTitle { get; set; }
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Process> Processes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessStepTemplate> StepTemplates { get; set; }
    	/// <summary>
    	/// Clones all properties in a new ProcessTemplate instance,
    	/// except PrimaryKey(s)
    	/// </summary>
    	/// <returns>New ProcessTemplate instance</returns>
        public ProcessTemplate Clone()
        {
            var clone = new ProcessTemplate();
            clone.TemplateName = TemplateName;
            clone.DefaultTitle = DefaultTitle;
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
    
    	// Use below function in a partial class file (eg. ProcessTemplate.part.cs)
    	// to add more complexity to clone
        partial void Cloning(ProcessTemplate clone);
    }
    
    //Data Transfer Object type for ProcessTemplate
    public partial class ProcessTemplateDTO
    {
        public ProcessTemplateDTO() { }
    
        public ProcessTemplateDTO(ProcessTemplate entity)
        {
            Id = entity.Id;
            TemplateName = entity.TemplateName;
            DefaultTitle = entity.DefaultTitle;
            IsApproved = entity.IsApproved;
            ApproveDate = entity.ApproveDate;
        
            Initiliazing(entity);
        }
    
        partial void Initiliazing(ProcessTemplate entity);
        
        public virtual ProcessTemplate ToProcessTemplate()
        {
            var entity = new ProcessTemplate();
            entity.Id = Id;
            entity.TemplateName = TemplateName;
            entity.DefaultTitle = DefaultTitle;
            entity.IsApproved = IsApproved;
            entity.ApproveDate = ApproveDate;
    
            Converting(entity);
    
            return entity;
        }
    
        partial void Converting(ProcessTemplate entity);
      
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string DefaultTitle { get; set; }
        public bool IsApproved { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
    }
}
