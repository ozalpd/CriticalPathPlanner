using CriticalPath.Web.Models;
using ImageResizer;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CriticalPath.Web.Controllers
{
    public class FilesController : BaseController
    {
        [Authorize]
        public ContentResult ImageUpload(HttpPostedFileBase file)
        {
            var filename = string.Format("{0}.{1}", Guid.NewGuid(), jpg);

            ResizeImage(file, AppSettings.Urls.ProductImages, filename, jpg);
            ResizeImage(file, AppSettings.Urls.ThumbImages, filename, jpg);

            return new ContentResult
            {
                ContentType = "text/plain",
                Content = filename,
                ContentEncoding = Encoding.UTF8
            };
        }

        protected virtual void ResizeImage(HttpPostedFileBase sourceFile, string targetFolder, string targetName, string format)
        {
            var target = string.Format("~{0}/{1}", targetFolder, targetName);

            var settings = GetResizeSettings(AppSettings.Settings.MaxImageWidht, AppSettings.Settings.MaxImageHeight, format);

            ImageJob job = new ImageJob(sourceFile, target, settings);
            job.CreateParentDirectory = true;
            job.Build();
        }
        protected static string png = "png";
        protected static string jpg = "jpg";

        protected virtual Instructions GetResizeSettings(int maxWidht, int maxHeight, string format)
        {
            string setting = string.Format("maxwidth={0};maxheight={1};format={2}", maxWidht, maxHeight, format);
            var instructions = new Instructions(setting);
            instructions.JpegQuality = 80;

            return instructions;
        }
    }
}