using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class PurchaseOrderCreateVM : PurchaseOrderDTO
    {
        public PurchaseOrderCreateVM(PurchaseOrder entity) : base(entity) { }
        public PurchaseOrderCreateVM() { }

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

        public int SizeRate1Id { get; set; }
        public int SizeRate2Id { get; set; }
        public int SizeRate3Id { get; set; }
        public int SizeRate4Id { get; set; }
        public int SizeRate5Id { get; set; }
        public int SizeRate6Id { get; set; }
        public int SizeRate7Id { get; set; }
        public int SizeRate8Id { get; set; }

        public int? Size1Rate { get; set; }
        public int? Size2Rate { get; set; }
        public int? Size3Rate { get; set; }
        public int? Size4Rate { get; set; }
        public int? Size5Rate { get; set; }
        public int? Size6Rate { get; set; }
        public int? Size7Rate { get; set; }
        public int? Size8Rate { get; set; }

        public string Size1Caption { get; set; }
        public string Size2Caption { get; set; }
        public string Size3Caption { get; set; }
        public string Size4Caption { get; set; }
        public string Size5Caption { get; set; }
        public string Size6Caption { get; set; }
        public string Size7Caption { get; set; }
        public string Size8Caption { get; set; }

        protected override void InitSizeRates()
        {
            base.InitSizeRates();
            for (int i = 1; i < 9; i++)
            {
                var sizeRate = GetSizeRate(i);
                if (sizeRate != null)
                {
                    SizeRates.Add(sizeRate);
                }
            }
        }

        #region Long Helper Methods GetSizeRate PutSizeRate
        protected SizeRateDTO GetSizeRate(int rateNr)
        {
            int id = 0;
            int rate = 0;
            string caption = string.Empty;

            switch (rateNr)
            {
                case 1:
                    id = SizeRate1Id;
                    rate = Size1Rate ?? 0;
                    caption = Size1Caption;
                    break;

                case 2:
                    id = SizeRate2Id;
                    rate = Size2Rate ?? 0;
                    caption = Size2Caption;
                    break;

                case 3:
                    id = SizeRate3Id;
                    rate = Size3Rate ?? 0;
                    caption = Size3Caption;
                    break;

                case 4:
                    id = SizeRate4Id;
                    rate = Size4Rate ?? 0;
                    caption = Size4Caption;
                    break;

                case 5:
                    id = SizeRate5Id;
                    rate = Size5Rate ?? 0;
                    caption = Size5Caption;
                    break;

                case 6:
                    id = SizeRate6Id;
                    rate = Size6Rate ?? 0;
                    caption = Size6Caption;
                    break;

                case 7:
                    id = SizeRate7Id;
                    rate = Size7Rate ?? 0;
                    caption = Size7Caption;
                    break;

                case 8:
                    id = SizeRate8Id;
                    rate = Size8Rate ?? 0;
                    caption = Size8Caption;
                    break;

                default:
                    break;
            }

            if(string.IsNullOrEmpty(caption)|| rate <= 0)
            {
                return null;
            }
            else
            {
                return new SizeRateDTO()
                {
                    Id = id,
                    Caption = caption,
                    Rate = rate,
                    DisplayOrder = rateNr * 1000,
                    PurchaseOrderId = Id
                };
            }
        }

        protected void PutSizeRate(int rateNr, SizeRate rate)
        {
            switch (rateNr)
            {
                case 1:
                    SizeRate1Id = rate.Id;
                    Size1Rate = rate.Rate;
                    Size1Caption = rate.Caption;
                    break;

                case 2:
                    SizeRate2Id = rate.Id;
                    Size2Rate = rate.Rate;
                    Size2Caption = rate.Caption;
                    break;

                case 3:
                    SizeRate3Id = rate.Id;
                    Size3Rate = rate.Rate;
                    Size3Caption = rate.Caption;
                    break;

                case 4:
                    SizeRate4Id = rate.Id;
                    Size4Rate = rate.Rate;
                    Size4Caption = rate.Caption;
                    break;

                case 5:
                    SizeRate5Id = rate.Id;
                    Size5Rate = rate.Rate;
                    Size5Caption = rate.Caption;
                    break;

                case 6:
                    SizeRate6Id = rate.Id;
                    Size6Rate = rate.Rate;
                    Size6Caption = rate.Caption;
                    break;

                case 7:
                    SizeRate7Id = rate.Id;
                    Size7Rate = rate.Rate;
                    Size7Caption = rate.Caption;
                    break;

                case 8:
                    SizeRate8Id = rate.Id;
                    Size8Rate = rate.Rate;
                    Size8Caption = rate.Caption;
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
