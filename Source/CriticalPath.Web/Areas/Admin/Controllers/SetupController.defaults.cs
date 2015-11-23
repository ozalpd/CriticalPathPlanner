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
        private async Task SeedCurrencies(StringBuilder sb)
        {
            int count = await DataContext.GetCurrencyQuery().CountAsync();
            if (count > 0)
            {
                sb.Append("Database already has ");
                sb.Append(count);
                sb.Append(" Currency records.<br>");
                return;
            }

            var currencies = GetCurrencies();
            foreach (var item in currencies)
            {
                DataContext.Currencies.Add(item);
            }

            sb.Append("<h4>");
            sb.Append(currencies.Length);
            sb.Append(' ');
            sb.Append(" Currency records added.</h4>");
        }

        private async Task SeedFreightTerms(StringBuilder sb)
        {
            int count = await DataContext.GetFreightTermQuery().CountAsync();
            if (count > 0)
            {
                sb.Append("Database already has ");
                sb.Append(count);
                sb.Append(" FreightTerm records.<br>");
                return;
            }

            var freightTerms = GetFreightTerms();
            foreach (var item in freightTerms)
            {
                DataContext.FreightTerms.Add(item);
            }
            sb.Append("<h4>");
            sb.Append(freightTerms.Length);
            sb.Append(' ');
            sb.Append(" FreightTerm records added.</h4>");
        }

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

        private FreightTerm[] GetFreightTerms()
        {
            FreightTerm[] terms =
            {
                new FreightTerm { IncotermCode="EXW" },
                new FreightTerm { IncotermCode="FCA" },
                new FreightTerm { IncotermCode="FAS" },
                new FreightTerm { IncotermCode="FOB", IsPublished=true },
                new FreightTerm { IncotermCode="CPT" },
                new FreightTerm { IncotermCode="CFR (CNF)" },
                new FreightTerm { IncotermCode="CIF", IsPublished=true },
                new FreightTerm { IncotermCode="CIP" },
                new FreightTerm { IncotermCode="DAT" },
                new FreightTerm { IncotermCode="DAP" },
                new FreightTerm { IncotermCode="DDP" }
            };

            return terms;
        }

        private Currency[] GetCurrencies()
        {
            Currency[] c = new Currency[]
            {
                new Currency { CurrencyName="Turkish Lira", CurrencyCode="TRY", CurrencySymbol="₺", IsPublished=true },
                new Currency { CurrencyName="United States Dollar", CurrencyCode="USD", CurrencySymbol="$", IsPublished=true },
                new Currency { CurrencyName="United Kingdom Pound", CurrencyCode="GBP", CurrencySymbol="£", IsPublished=true },
                new Currency { CurrencyName="Euro Member Countries", CurrencyCode="EUR", CurrencySymbol="€", IsPublished=true },
                new Currency { CurrencyName="Canada Dollar", CurrencyCode="CAN", CurrencySymbol="$", IsPublished=true }
            };

            return c;
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
