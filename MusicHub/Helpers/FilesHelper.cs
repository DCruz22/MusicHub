using System;
using System.IO;
using ImageResizer;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicHub.Helpers
{
    public class FilesHelper
    {
        private const string CMS_TEMP_PATH = "public\\temp";
        private const string CMS_FINAL_PATH_POST = "public\\post_images";
        private const string CMS_FINAL_PATH_PROFILE = "public\\profile_pictures";

        public static string SaveFiles(HttpFileCollectionBase files, string tipo)
        {

            string filename = "",
                   ext = "",
                   savedFileName = "";

            foreach (string file in files)
            {
                HttpPostedFileBase hpf = (HttpPostedFileBase)files[file];

                if (hpf.ContentLength == 0)
                    continue;

                DateTime now = DateTime.Now;
                ext = Path.GetExtension(hpf.FileName);
                filename = tipo + "-" + now.Millisecond + now.Second + now.Hour + now.Minute + now.Day + now.Year + ext;

                savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + CMS_TEMP_PATH, filename);
                hpf.SaveAs(savedFileName);
            }

            return "~\\" + CMS_TEMP_PATH + "\\" + filename;
        }

        public static string SaveFile(HttpPostedFileBase hpf, string type)
        {

            string filename = "",
                   ext = "",
                   savedFileName = "";

            if (hpf.ContentLength == 0)
                return "";

            var settings = new ResizeSettings
            {
                MaxWidth = 450,
                MaxHeight = 350,
                Format = "jpg"
            };


            DateTime now = DateTime.Now;
            ext = Path.GetExtension(hpf.FileName);
            filename = type + "-" + now.Millisecond + now.Second + now.Hour + now.Minute + now.Day + now.Year + ext;

            savedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + CMS_TEMP_PATH, filename);

            ImageBuilder.Current.Build(hpf.InputStream, savedFileName, settings);
            //hpf.SaveAs(savedFileName);

            return filename;
        }

        public static string movePostFile(string fileName, Photo_types photoType)
        {

            string finalPath = (photoType == Photo_types.POST_PICTURE ? CMS_FINAL_PATH_POST : CMS_FINAL_PATH_PROFILE);

            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + CMS_TEMP_PATH, fileName);
            string destinationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + finalPath, fileName);

            File.Copy(sourceFilePath, destinationFilePath);

            return ("~\\" + finalPath + "\\" + fileName);
        }

        public static Boolean isImageFound(HttpFileCollectionBase files)
        {

            foreach (string file in files)
            {
                HttpPostedFileBase hpf = (HttpPostedFileBase)files[file];

                if (hpf.ContentLength == 0)
                    return false;
            }

            return true;
        }

        public enum Photo_types
        {

            POST_PICTURE,
            PROFILE_PICTURE
        }

    }
}