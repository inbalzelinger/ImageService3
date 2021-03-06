﻿using communication.Client;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Data;

namespace ContosoUniversity.Models
{
    public class ExecuteDeleteModel
    {
        private IClient m_client;
        private bool finish;
        
        private string m_imagePath;
        private string m_thumbPath;


        public ExecuteDeleteModel(string imagePath,string thumbPath)
        {
            this.m_imagePath=imagePath;
            this.m_thumbPath = thumbPath;
        }

        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ImagePath: ")]
        public string ImagePath
        {
            get { return this.m_imagePath; }
            set { this.m_imagePath= value; }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "thumbPath: ")]
        public string ThumbPath
        {
            get { return this.m_thumbPath; }
            set { this.m_thumbPath = value; }
        }

        public void DeleteAPhoto()
        {
            File.Delete(ImagePath);
            File.Delete(ThumbPath);
            Console.WriteLine("delete photo");
        }
    }
}

      

    


