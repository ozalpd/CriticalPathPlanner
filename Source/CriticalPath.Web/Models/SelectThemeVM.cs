using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class SelectThemeVM
    {
        public string Theme { get; set; }
        public string[] ThemeList
        {
            get { return AppSettings.Themes; }
        }

        public string GetThemName(string theme)
        {
            string[] s = theme.Split('/');
            return s.LastOrDefault();
        }
    }
}
