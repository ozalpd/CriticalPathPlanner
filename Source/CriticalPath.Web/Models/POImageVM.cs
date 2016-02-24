using CriticalPath.Data;

namespace CriticalPath.Web.Models
{
    public class POImageVM : POImageDTO
    {
        public POImageVM() { }
        public POImageVM(POImage image):base(image) { }

        protected override void Constructing(POImage image)
        {
            base.Constructing(image);
            if (image.PurchaseOrder != null)
            {
                PurchaseOrder = new PurchaseOrderDTO(image.PurchaseOrder);
            }
        }

        public override POImage ToPOImage()
        {
            var image = base.ToPOImage();
            if (!string.IsNullOrEmpty(ChangedImageFile))
            {
                image.ImageUrl = ChangedImageFile;
            }
            return image;
        }

        public string ChangedImageFile { get; set; }

        public PurchaseOrderDTO PurchaseOrder { get; set; }
    }
}