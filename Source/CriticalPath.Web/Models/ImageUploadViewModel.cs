namespace CriticalPath.Web.Models
{
    public class ImageUploadViewModel
    {
        public ImageUploadViewModel()
        {
            UploadUrl = AppSettings.Urls.ImageUpload;
            ThumbFolderUrl = AppSettings.Urls.ThumbImages;
            ImageFolderUrl = AppSettings.Urls.ProductImages;
        }

        public ImageUploadViewModel(string propertyName)
            : this()
        {
            PropertyName = propertyName;
        }

        public ImageUploadViewModel(string propertyName, string imageName)
            : this(propertyName)
        {
            ImageName = imageName;
        }

        public int Id { get; set; }

        public string PropertyName { get; set; }

        public string ImageFolderUrl { get; set; }
        public string ImageName { get; set; }

        public bool MultiUpload { get; set; }
        public string ThumbFolderUrl { get; set; }
        public string UploadUrl { get; set; }
    }
}