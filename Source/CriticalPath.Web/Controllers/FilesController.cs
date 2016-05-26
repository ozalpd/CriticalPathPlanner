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
        public JsonResult ImageUpload(HttpPostedFileBase file)
        {
            var filename = string.Format("{0}.{1}", Guid.NewGuid(), jpg);

            var settings = GetResizeSettings(AppSettings.Settings.MaxImageWidht, AppSettings.Settings.MaxImageHeight, jpg);
            ResizeImage(file, AppSettings.Urls.ProductImages, filename, settings);

            settings = GetResizeSettings(AppSettings.Settings.MaxThumbWidht, AppSettings.Settings.MaxThumbHeight, jpg);
            ResizeImage(file, AppSettings.Urls.ThumbImages, filename, settings);

            return Json(new { filename = filename });
        }

        protected virtual void ResizeImage(HttpPostedFileBase sourceFile, string targetFolder, string targetName, Instructions settings)
        {
            var target = string.Format("~{0}/{1}", targetFolder, targetName);

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