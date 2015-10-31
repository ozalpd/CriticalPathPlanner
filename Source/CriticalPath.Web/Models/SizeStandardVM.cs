using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class SizeStandardVM : SizeStandardDTO
    {
        public SizeStandardVM() { }
        public SizeStandardVM(SizeStandard entity):base(entity) { }

        protected override void Constructing(SizeStandard entity)
        {
            base.Constructing(entity);
            int i = 0;
            foreach (var item in SizeCaptions)
            {
                i++;
                SetCaption(item, i);
            }
        }

        public override SizeStandard ToSizeStandard()
        {
            for (int i = 1; i < 9; i++)
            {
                var sizeCaption = GetCaption(i);
                if (sizeCaption != null)
                    SizeCaptions.Add(sizeCaption);
            }
            return base.ToSizeStandard();
        }

        #region Long Helper Methods SetCaption GetCaption
        protected void SetCaption(SizeCaptionDTO sizeCaption, int captionNr)
        {
            if (string.IsNullOrEmpty(sizeCaption?.Caption))
                return;
            switch (captionNr)
            {
                case 1:
                    Size1Caption = sizeCaption.Caption;
                    Size1CaptionId = sizeCaption.Id;
                    break;

                case 2:
                    Size2Caption = sizeCaption.Caption;
                    Size2CaptionId = sizeCaption.Id;
                    break;

                case 3:
                    Size3Caption = sizeCaption.Caption;
                    Size3CaptionId = sizeCaption.Id;
                    break;

                case 4:
                    Size4Caption = sizeCaption.Caption;
                    Size4CaptionId = sizeCaption.Id;
                    break;

                case 5:
                    Size5Caption = sizeCaption.Caption;
                    Size5CaptionId = sizeCaption.Id;
                    break;

                case 6:
                    Size6Caption = sizeCaption.Caption;
                    Size6CaptionId = sizeCaption.Id;
                    break;

                case 7:
                    Size7Caption = sizeCaption.Caption;
                    Size7CaptionId = sizeCaption.Id;
                    break;

                case 8:
                    Size8Caption = sizeCaption.Caption;
                    Size8CaptionId = sizeCaption.Id;
                    break;

                default:
                    break;
            }
        }

        protected SizeCaptionDTO GetCaption(int captionNr)
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

                default:
                    break;
            }

            if (string.IsNullOrEmpty(caption) && captionId == 0)
            {
                return null;
            }
            else
            {
                return new SizeCaptionDTO()
                {
                    Id = captionId,
                    Caption = caption,
                    DisplayOrder = captionNr * 1000,
                    SizeStandardId = Id
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

        public string Size1Caption { get; set; }
        public string Size2Caption { get; set; }
        public string Size3Caption { get; set; }
        public string Size4Caption { get; set; }
        public string Size5Caption { get; set; }
        public string Size6Caption { get; set; }
        public string Size7Caption { get; set; }
        public string Size8Caption { get; set; }
    }
}
