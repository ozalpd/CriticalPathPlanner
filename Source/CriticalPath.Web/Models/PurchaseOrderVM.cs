using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class PurchaseOrderVM : PurchaseOrderDTO
    {
        protected override void Constructing(PurchaseOrder entity)
        {
            base.Constructing(entity);
            int i = 0;
            var rates = entity.QuantitySizeRates.OrderBy(r => r.DisplayOrder);
            foreach (var rate in rates)
            {
                i++;
                PutSizeRate(i, rate);
            }
        }

        public int QuantitySize1Rate { get; set; }
        public int QuantitySize2Rate { get; set; }
        public int QuantitySize3Rate { get; set; }
        public int QuantitySize4Rate { get; set; }
        public int QuantitySize5Rate { get; set; }
        public int QuantitySize6Rate { get; set; }
        public int QuantitySize7Rate { get; set; }
        public int QuantitySize8Rate { get; set; }

        public string Size1Caption { get; set; }
        public string Size2Caption { get; set; }
        public string Size3Caption { get; set; }
        public string Size4Caption { get; set; }
        public string Size5Caption { get; set; }
        public string Size6Caption { get; set; }
        public string Size7Caption { get; set; }
        public string Size8Caption { get; set; }

        public int QuantitySizeRateTotal { get; set; }



        protected void PutSizeRate(int rateNr, QuantitySizeRate rate)
        {
            switch (rateNr)
            {
                case 1:
                    QuantitySize1Rate = rate.Rate;
                    Size1Caption = rate.SizeCaption?.Caption;
                    break;

                case 2:
                    QuantitySize2Rate = rate.Rate;
                    Size2Caption = rate.SizeCaption?.Caption;
                    break;

                case 3:
                    QuantitySize3Rate = rate.Rate;
                    Size3Caption = rate.SizeCaption?.Caption;
                    break;

                case 4:
                    QuantitySize4Rate = rate.Rate;
                    Size4Caption = rate.SizeCaption?.Caption;
                    break;

                case 5:
                    QuantitySize5Rate = rate.Rate;
                    Size5Caption = rate.SizeCaption?.Caption;
                    break;

                case 6:
                    QuantitySize6Rate = rate.Rate;
                    Size6Caption = rate.SizeCaption?.Caption;
                    break;

                case 7:
                    QuantitySize7Rate = rate.Rate;
                    Size7Caption = rate.SizeCaption?.Caption;
                    break;

                case 8:
                    QuantitySize8Rate = rate.Rate;
                    Size8Caption = rate.SizeCaption?.Caption;
                    break;

                default:
                    break;
            }
        }
    }
}
