using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class SizingStandardVM : SizingStandardDTO
    {
        public SizingStandardVM() { }
        public SizingStandardVM(SizingStandard entity):base(entity) { }

        protected override void Constructing(SizingStandard entity)
        {
            base.Constructing(entity);
            int i = 0;
            foreach (var item in Sizings)
            {
                i++;
                SetSizing(item, i);
            }
        }

        public override SizingStandard ToSizingStandard()
        {
            for (int i = 1; i < 13; i++)
            {
                var sizing = GetCaption(i);
                if (sizing != null)
                    Sizings.Add(sizing);
            }
            return base.ToSizingStandard();
        }

        #region Long Helper Methods SetCaption GetCaption
        protected void SetSizing(SizingDTO sizing, int captionNr)
        {
            if (string.IsNullOrEmpty(sizing?.Caption))
                return;
            switch (captionNr)
            {
                case 1:
                    Size1Caption = sizing.Caption;
                    Size1CaptionId = sizing.Id;
                    break;

                case 2:
                    Size2Caption = sizing.Caption;
                    Size2CaptionId = sizing.Id;
                    break;

                case 3:
                    Size3Caption = sizing.Caption;
                    Size3CaptionId = sizing.Id;
                    break;

                case 4:
                    Size4Caption = sizing.Caption;
                    Size4CaptionId = sizing.Id;
                    break;

                case 5:
                    Size5Caption = sizing.Caption;
                    Size5CaptionId = sizing.Id;
                    break;

                case 6:
                    Size6Caption = sizing.Caption;
                    Size6CaptionId = sizing.Id;
                    break;

                case 7:
                    Size7Caption = sizing.Caption;
                    Size7CaptionId = sizing.Id;
                    break;

                case 8:
                    Size8Caption = sizing.Caption;
                    Size8CaptionId = sizing.Id;
                    break;

                case 9:
                    Size9Caption = sizing.Caption;
                    Size9CaptionId = sizing.Id;
                    break;

                case 10:
                    Size10Caption = sizing.Caption;
                    Size10CaptionId = sizing.Id;
                    break;

                case 11:
                    Size11Caption = sizing.Caption;
                    Size11CaptionId = sizing.Id;
                    break;

                case 12:
                    Size12Caption = sizing.Caption;
                    Size12CaptionId = sizing.Id;
                    break;

                default:
                    break;
            }
        }

        protected SizingDTO GetCaption(int captionNr)
        {
            int captionId = 0;
            string caption = string.Empty;
            switch (captionNr)
            {
                case 1:
                    captionId = Size1CaptionId;
                    caption = Size1Caption;
                    break;

                case 2:
                    captionId = Size2CaptionId;
                    caption = Size2Caption;
                    break;

                case 3:
                    captionId = Size3CaptionId;
                    caption = Size3Caption;
                    break;

                case 4:
                    captionId = Size4CaptionId;
                    caption = Size4Caption;
                    break;

                case 5:
                    captionId = Size5CaptionId;
                    caption = Size5Caption;
                    break;

                case 6:
                    captionId = Size6CaptionId;
                    caption = Size6Caption;
                    break;

                case 7:
                    captionId = Size7CaptionId;
                    caption = Size7Caption;
                    break;

                case 8:
                    captionId = Size8CaptionId;
                    caption = Size8Caption;
                    break;

                case 9:
                    captionId = Size9CaptionId;
                    caption = Size9Caption;
                    break;

                case 10:
                    captionId = Size10CaptionId;
                    caption = Size10Caption;
                    break;

                case 11:
                    captionId = Size11CaptionId;
                    caption = Size11Caption;
                    break;

                case 12:
                    captionId = Size12CaptionId;
                    caption = Size12Caption;
                    break;

                default:
                    break;
            }

            if (string.IsNullOrEmpty(caption) && captionId == 0)
            {
                return null;
            }
            else
            {
                return new SizingDTO()
                {
                    Id = captionId,
                    Caption = caption,
                    DisplayOrder = captionNr * 1000,
                    SizingStandardId = Id
                };
            }
        }
        #endregion

        public int Size1CaptionId { get; set; }
        public int Size2CaptionId { get; set; }
        public int Size3CaptionId { get; set; }
        public int Size4CaptionId { get; set; }
        public int Size5CaptionId { get; set; }
        public int Size6CaptionId { get; set; }
        public int Size7CaptionId { get; set; }
        public int Size8CaptionId { get; set; }
        public int Size9CaptionId { get; set; }
        public int Size10CaptionId { get; set; }
        public int Size11CaptionId { get; set; }
        public int Size12CaptionId { get; set; }

        public string Size1Caption { get; set; }
        public string Size2Caption { get; set; }
        public string Size3Caption { get; set; }
        public string Size4Caption { get; set; }
        public string Size5Caption { get; set; }
        public string Size6Caption { get; set; }
        public string Size7Caption { get; set; }
        public string Size8Caption { get; set; }
        public string Size9Caption { get; set; }
        public string Size10Caption { get; set; }
        public string Size11Caption { get; set; }
        public string Size12Caption { get; set; }
    }
}
