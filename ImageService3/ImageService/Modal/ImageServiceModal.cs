﻿using ImageService.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string m_OutputFolder;  // The Output Folder
        private int m_thumbnailSize;   // The Size Of The Thumbnail Size
        private static Regex r = new Regex(":");
        private int m_imageCount;
        #endregion
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m_OutputFolder">The Path of the destination foldr</param>
        /// <param name="m_thumbnailSize">size of thumbnail pic </param>
        public ImageServiceModal(string m_OutputFolder, int m_thumbnailSize)
        {
            this.m_OutputFolder = m_OutputFolder;
            this.m_thumbnailSize = m_thumbnailSize;
            Directory.CreateDirectory(m_OutputFolder);
            new FileInfo(m_OutputFolder).Attributes = new FileInfo(m_OutputFolder).Attributes | FileAttributes.Hidden;
            this.m_imageCount = Count(this.m_OutputFolder);
        }

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
        public int ImageCount
        {
            get
            {
                return m_imageCount;
            }
            set
            {
                m_imageCount = value;
            }
        }
        ///</summary>
        ///create folders according  to date if
        ///needed and add the image in path
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        public string AddFile(string path, out bool result)
        {
            result = true;

            DateTime dateTime;

            string imageName = Path.GetFileName(path);


            try
            {
                dateTime = GetDateTakenFromImage(path);
            }
            catch (Exception e)
            {
                dateTime = File.GetCreationTime(path);
            }

            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();

            string yearPath = this.m_OutputFolder + "\\" + year;
            string monthPath = yearPath + "\\" + month;

            string yearThumbPath = this.m_OutputFolder + "\\" + "thumbNail" + '\\' + year;
            string monthTumbPath = yearThumbPath + "\\" + month;

            string forThumb = monthTumbPath + "\\" + Path.GetFileName(path);
            string forImage = monthPath + "\\" + Path.GetFileName(path);

            string changeSameName = "";

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


            changeSameName = CheckName(path, monthTumbPath);

            try
            {
                Image image = Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
                image = (Image)(new Bitmap(image, new Size(m_thumbnailSize, m_thumbnailSize)));
                image.Save(changeSameName);
                changeSameName = CheckName(path, monthPath);
                File.Copy(path, changeSameName);
                File.Delete(path);
            }
            catch (Exception e)
            {
                result = false;
                return "save the thimnbNail or copy the picture";
            }
            this.m_imageCount++;
            return "seccesfully added";
        }
        ///<summary>
        ///return the name of the image file  
        /// </summary>
        /// <param name="pathFile">The Path of image </param>
        ///<param name="pathDir">The Path of destination dir</param>
        /// <returns>the path of the image</returns>
        private string CheckName(string pathFile, string pathDir)
        {
            int count = 0;
            string fileNamePath = pathDir + "\\" + Path.GetFileName(pathFile);

            while (File.Exists(fileNamePath))
            {
                count++;
                fileNamePath = pathDir + "\\" + Path.GetFileNameWithoutExtension(pathFile) + "(" + count.ToString() + ")" + Path.GetExtension(pathFile);
            }
            return fileNamePath;
        }
        ///<summary>
        ///return the date of the image in path
        /// </summary>
        /// <param name="path">The Path of the Image file</param>
        /// <returns>date of image</returns>
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

        public string GetConfigFile(out bool result)
        {
            JObject j = new JObject();
            j["SourceName"] = ConfigurationManager.AppSettings["SourceName"];
            j["ThumbnailSize"] = ConfigurationManager.AppSettings["ThumbnailSize"];
            j["LogName"] = ConfigurationManager.AppSettings["LogName"];
            j["Handler"] = ConfigurationManager.AppSettings["Handler"];
            j["OutputDir"] = ConfigurationManager.AppSettings["OutputDir"];
            result = true;
            string ret = "Config " + j.ToString().Replace(Environment.NewLine, " ");
            return ret;
        }


        public string CloseHandlerCommand(out bool result)
        {
            throw new NotImplementedException();
        }

        public string GetImageWebDetails(out bool result)

        {
            string status = " Not Running";
            if (ImageService3.ServiceStatusClass.isRunnig)
            {
                status = "Runnig";
            }
            JObject j = new JObject();
            j["ImageCount"] = ImageCount.ToString();
            j["ServiceStatus"] = status;
            result = true;
            string ret = "ImageWeb " + j.ToString().Replace(Environment.NewLine, " ");
            
            return ret;
        }

        public static int Count(string dirPath)
        {
          
            string[] files = Directory.GetFiles(dirPath,"*", SearchOption.AllDirectories);
            string[] thumbs = Directory.GetFiles(dirPath + "\\" + "thumbNail", "*", SearchOption.AllDirectories);
            return files.Length - thumbs.Length;
           
        }
        }
    }


    
