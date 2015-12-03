using CP.i8n;
using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CriticalPath.Web.Models
{
    public class DiscontinuedVM : IDiscontinued
    {
        public DiscontinuedVM() { }
        public DiscontinuedVM(IDiscontinued entity)
        {
            Discontinued = entity.Discontinued;
            DiscontinueDate = entity.DiscontinueDate;
            DiscontinueNotes = entity.DiscontinueNotes;
        }

        [UIHint("BoolRed")]
        [Display(ResourceType = typeof(EntityStrings), Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(EntityStrings), Name = "DiscontinueDate")]
        public DateTime? DiscontinueDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorStrings), ErrorMessageResourceName = "Required")]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(EntityStrings), Name = "DiscontinueNotes")]
        public string DiscontinueNotes { get; set; }
    }
}