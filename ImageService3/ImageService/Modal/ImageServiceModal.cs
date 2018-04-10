using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;   // The Size Of The Thumbnail Size
        #endregion

        public string OutputFolder
        {
            get
            {
                return m_OutputFolder;
            }
            set
            {
                m_OutputFolder = value;
            }
        }

        public int ThumbnailSize
        {
            get
            {
                return m_thumbnailSize;
            }
            set
            {
                m_thumbnailSize = value;
            }
        }


        public string AddFile(string path, out bool result)
        {
            result = true;
            DateTime dateTime = File.GetCreationTime(path);
            int year = dateTime.Year;
            int month = dateTime.Month;

            string imageName = Path.GetFileName(path);


            string yearPath = this.m_OutputFolder + "\\" + year.ToString();
            string monthPath = yearPath + "\\" + month.ToString();

            string yearThumbPath = this.m_OutputFolder + "\\" + "thumbNail" + '\\' + year.ToString();
            string monthTumbPath = yearThumbPath + "\\" + month.ToString();

            //create the thumb picture.

            //check if the thombNail directory exist
            if (!Directory.Exists(this.m_OutputFolder + "\\" + "thumbNail"))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(this.m_OutputFolder, "thumbNail"));
                }
                catch (Exception e)
                {
                    result = false;
                    return "fail to create thumbNail folder";
                }
            }
            //check if the year directory exist
            if (!Directory.Exists(yearPath))
            {
                try
                {
                    Directory.CreateDirectory(yearPath);
                    Directory.CreateDirectory(yearThumbPath);
                }
                catch (Exception e)
                {
                    result = false;
                    return "fail to create year folder";
                }
            }
            //check if the month directory exist
            if (!Directory.Exists(monthPath))
            {
                try
                {
                    Directory.CreateDirectory(monthPath);
                    Directory.CreateDirectory(monthTumbPath);
                }
                catch (Exception e)
                {
                    result = false;
                    return "fail to create month folder";
                }
            }

            //move the file and save the thumbNail picture.
            try
            {
                Image image = Image.FromFile(path);
                Image thumb = image.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);

                thumb.Save(Path.ChangeExtension(monthTumbPath, imageName));
                File.Move(path, monthPath);
            }
            catch (Exception e)
            {
                result = false;
                return "save the thimnbNail or copy the picture";
            }
            return "seccesfully added";
        }
    }
}