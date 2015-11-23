using CriticalPath.Data;

namespace CriticalPath.Web.Areas.Admin.Models
{
    public class CurrencyEditVM : CurrencyDTO
    {
        public CurrencyEditVM() { }
        public CurrencyEditVM(Currency entity) : base(entity)
        {
            IsPublished = entity.IsPublished;
        }

        public bool IsPublished { get; set; }
    }
}
