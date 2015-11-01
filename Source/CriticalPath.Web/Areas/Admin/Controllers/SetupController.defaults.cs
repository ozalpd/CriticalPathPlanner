using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Areas.Admin.Controllers
{
    public partial class SetupController
    {
        private async Task SeedSizingStandards(StringBuilder sb)
        {
            int count = await DataContext.GetSizingStandardQuery().CountAsync();
            if (count > 0)
            {
                sb.Append("Database already has ");
                sb.Append(count);
                sb.Append(" SizingStandard records.<br>");
                return;
            }
            var sizingStandards = GetSizingStandards();
            foreach (var item in sizingStandards)
            {
                DataContext.SizingStandards.Add(item);
                sb.Append("SizingStandard: ");
                sb.Append(item.Title);
                sb.Append(" added<br>");
            }
            sb.Append("<h4>");
            sb.Append(sizingStandards.Length);
            sb.Append(' ');
            sb.Append(" SizingStandard records added.</h4>");
        }

        private SizingStandard[] GetSizingStandards()
        {
            SizingStandard[] sizingStandards = {
                new SizingStandard
                {
                    Title = "Letters XS-4XL",
                    Sizings = {
                        new Sizing() { DisplayOrder=1000,Caption="XS"},
                        new Sizing() { DisplayOrder=2000,Caption="S"},
                        new Sizing() { DisplayOrder=3000,Caption="M"},
                        new Sizing() { DisplayOrder=4000,Caption="L"},
                        new Sizing() { DisplayOrder=5000,Caption="XL"},
                        new Sizing() { DisplayOrder=6000,Caption="XXL"},
                        new Sizing() { DisplayOrder=7000,Caption="3XL"},
                        new Sizing() { DisplayOrder=8000,Caption="4XL"}
                    }
                },
                new SizingStandard
                {
                    Title = "Children 4-8",
                    Sizings = {
                        new Sizing() { DisplayOrder=1000,Caption="4"},
                        new Sizing() { DisplayOrder=2000,Caption="6"},
                        new Sizing() { DisplayOrder=3000,Caption="8"}
                    }
                },
                new SizingStandard
                {
                    Title = "Children 6-12",
                    Sizings = {
                        new Sizing() { DisplayOrder=1000,Caption="6"},
                        new Sizing() { DisplayOrder=2000,Caption="8"},
                        new Sizing() { DisplayOrder=3000,Caption="10"},
                        new Sizing() { DisplayOrder=4000,Caption="12"}
                    }
                }
            };

            return sizingStandards;
        }
    }
}
