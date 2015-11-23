using CriticalPath.Data;

namespace CriticalPath.Web.Areas.Admin.Models
{
    public class FreightTermEditVM : FreightTermDTO
    {
        public FreightTermEditVM() { }
        public FreightTermEditVM(FreightTerm entity) : base(entity)
        {
            IsPublished = entity.IsPublished;
        }

        public bool IsPublished { get; set; }
    }
}
