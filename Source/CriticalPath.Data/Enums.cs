using CP.i8n;
using System.ComponentModel.DataAnnotations;

namespace CriticalPath.Data
{
    public enum DefaultJobPositions
    {
        [Display(ResourceType = typeof(EntityStrings), Name = "Designer")]
        Designer = 1,
        Merchandiser = 2
    }
}
