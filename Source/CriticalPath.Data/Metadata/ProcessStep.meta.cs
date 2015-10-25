using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CriticalPath.Data.Resources;
//------------------------------------------------------------------------------
//
//     This code was generated by OzzCodeGen.
//
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//------------------------------------------------------------------------------

namespace CriticalPath.Data
{
    [MetadataTypeAttribute(typeof(ProcessStep.ProcessStepMetadata))]
    public partial class ProcessStep
	{
        internal sealed partial class ProcessStepMetadata
		{
            // This metadata class is not intended to be instantiated.
            private ProcessStepMetadata() { }

            [StringLength(128, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "Title")]
            public string Title { get; set; }

            [StringLength(256, ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "MaxLeght")]
            [DataType(DataType.MultilineText)]
            [Display(ResourceType = typeof(EntityStrings), Name = "Description")]
            public string Description { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "IsCompleted")]
            public bool IsCompleted { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "DisplayOrder")]
            public int DisplayOrder { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "ProcessId")]
            public int ProcessId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "Process")]
            public Process Process { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "TargetStartDate")]
            public DateTime TargetStartDate { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "TargetEndDate")]
            public DateTime TargetEndDate { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "ForecastStartDate")]
            public DateTime ForecastStartDate { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "ForecastEndDate")]
            public DateTime ForecastEndDate { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "RealizedStartDate")]
            public DateTime RealizedStartDate { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "RealizedEndDate")]
            public DateTime RealizedEndDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
            [Display(ResourceType = typeof(EntityStrings), Name = "TemplateId")]
            public int TemplateId { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "Template")]
            public ProcessStepTemplate Template { get; set; }

            [Display(ResourceType = typeof(EntityStrings), Name = "IsApproved")]
            public bool IsApproved { get; set; }

            [DataType(DataType.Date)]
            [Display(ResourceType = typeof(EntityStrings), Name = "ApproveDate")]
            public DateTime ApproveDate { get; set; }

		}
	}
}