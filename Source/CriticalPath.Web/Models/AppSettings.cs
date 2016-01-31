﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriticalPath.Web.Models
{
    public class AppSettings
    {
        public static AppSettings Settings
        {
            get
            {
                if (_settings == null)
                    LoadSettings();
                return _settings;
            }
        }
        private static AppSettings _settings;

        private static void LoadSettings()
        {
            _settings = new AppSettings();
            //TODO: Load settings from DB
        }


        public int MaxImageHeight
        {
            get { return 800; }
        }

        public int MaxImageWidht
        {
            get { return 800; }
        }

        public int MaxThumbHeight
        {
            get { return 80; }
        }

        public int MaxThumbWidht
        {
            get { return 80; }
        }

        public bool ShowRetailPrice
        {
            get { return false; }
        }

        public bool ShowSecondaryPrices
        {
            get { return true; }
        }

        public static class Urls
        {
            public static string BigImageUpload = "/Files/BigImageUpload";
            public static string ImageUpload = "/Files/ImageUpload";
            public static string NoImageAvailable = "/images/no_image_found.png";
            public static string ProductImages = "/ProductFiles/Images";
            public static string ThumbImages = "/ProductFiles/Thumbs";
            public static string TempUploads = "/TempUploads";
        }

        public string SelectedTheme
        {
            set
            {
                if (SelectedTheme.Equals(value))
                    return;
                _selectedTheme = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_selectedTheme))
                    _selectedTheme = "~/Content/Cosmo";
                return _selectedTheme;
            }
        }
        private static string _selectedTheme;



        //TODO: Load themes from DB
        public static string[] Themes =
        {
            "~/Content/Slate",
            "~/Content/Slate-Ligth",
            "~/Content/Darkly",
            "~/Content/Cosmo",
            "~/Content/Flatly",
            "~/Content/Superhero",

            "~/Content/Default",
            "~/Content/Default-Darker",
            "~/Content/Default-Blue",
            "~/Content/Default-Red",
        };
    }
}
