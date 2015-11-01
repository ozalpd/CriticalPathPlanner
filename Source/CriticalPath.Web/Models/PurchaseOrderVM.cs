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
        public PurchaseOrderVM(PurchaseOrder entity) : base(entity) { }
        public PurchaseOrderVM() { }

        protected override void Constructing(PurchaseOrder entity)
        {
            base.Constructing(entity);
            int i = 0;
            var rates = entity.SizeRates.OrderBy(r => r.DisplayOrder);
            foreach (var rate in rates)
            {
                i++;
                PutSizeRate(i, rate);
            }
        }

        public int Size1Rate { get; set; }
        public int Size2Rate { get; set; }
        public int Size3Rate { get; set; }
        public int Size4Rate { get; set; }
        public int Size5Rate { get; set; }
        public int Size6Rate { get; set; }
        public int Size7Rate { get; set; }
        public int Size8Rate { get; set; }

        public string Size1Caption { get; set; }
        public string Size2Caption { get; set; }
        public string Size3Caption { get; set; }
        public string Size4Caption { get; set; }
        public string Size5Caption { get; set; }
        public string Size6Caption { get; set; }
        public string Size7Caption { get; set; }
        public string Size8Caption { get; set; }

        protected void PutSizeRate(int rateNr, SizeRate rate)
        {
            switch (rateNr)
            {
                case 1:
                    Size1Rate = rate.Rate;
                    Size1Caption = rate.Caption;
                    break;

                case 2:
                    Size2Rate = rate.Rate;
                    Size2Caption = rate.Caption;
                    break;

                case 3:
                    Size3Rate = rate.Rate;
                    Size3Caption = rate.Caption;
                    break;

                case 4:
                    Size4Rate = rate.Rate;
                    Size4Caption = rate.Caption;
                    break;

                case 5:
                    Size5Rate = rate.Rate;
                    Size5Caption = rate.Caption;
                    break;

                case 6:
                    Size6Rate = rate.Rate;
                    Size6Caption = rate.Caption;
                    break;

                case 7:
                    Size7Rate = rate.Rate;
                    Size7Caption = rate.Caption;
                    break;

                case 8:
                    Size8Rate = rate.Rate;
                    Size8Caption = rate.Caption;
                    break;

                default:
                    break;
            }
        }
    }
}
