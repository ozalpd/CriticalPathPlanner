using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class AppSettings
    {

        public static string SelectedTheme
        {
            set
            {
                if (SelectedTheme.Equals(value))
                    return;
                _selectedTheme = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_selectedTheme))
                    _selectedTheme = "~/Content/Slate-Ligth";
                return _selectedTheme;
            }
        }
        private static string _selectedTheme;




        public static string[] Themes =
        {
            "~/Content/Slate",
            "~/Content/Slate-Ligth",
            "~/Content/Darkly",
            "~/Content/Cosmo",
            "~/Content/Flatly",
            "~/Content/Superhero",

            "~/Content/Default",
            "~/Content/Default-Darker",
            "~/Content/Default-Blue",
            "~/Content/Default-Red",
        };
    }
}
