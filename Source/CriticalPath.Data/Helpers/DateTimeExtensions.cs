using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Data.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToToShortDateOrShortTimeString(this DateTime time)
        {
            return time.Date.Equals(DateTime.Now.Date) ?
                time.ToString("HH:mm") :
                time.ToString("dd/MM/yyyy");
        }
    }
}
