using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data
{
    public partial class CriticalPathContext
    {
        public async Task<string> CreatePoNr(DateTime orderDate)
        {
            int year = orderDate.Year;
            int month = orderDate.Month;

            var minDate = new DateTime(year, month, 1);
            var maxDate = minDate.AddMonths(1);
            var count = await PurchaseOrders
                        .Where(p => p.OrderDate >= minDate && p.OrderDate < maxDate)
                        .CountAsync();

            return string.Format("{0}{1:D2}-{2:D4}", (year - 2000), month, count + 1);
        }
    }
}
