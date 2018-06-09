using communication.Client;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Windows.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Models
{
    public class ImageWebModel

    {
        private IClient m_client;

        //public ImageWebModel()
        //{

        //    try
        //    {
        //        this.m_client = Client.ClientInstance;

        //        this.m_client.OnMessageRecived += GetMessageFromClient;

        //        StudentsList = new ObservableCollection<Student>();
        //        Student hadar = new Student();
        //        Student inbal = new Student();
        //        StudentsList.Add()

        //        SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
        //    }
        //    catch
        //    {
        //        Console.Write("Error");
        //    }
        //}

        //private void GetMessageFromClient(object sender, string message)
        //{
        //    if (message.Contains("ImageWeb "))
        //    {
        //        Debug.WriteLine("Working on config...");
        //        int i = message.IndexOf(" ") + 1;
        //        message = message.Substring(i);
        //        JObject json = JObject.Parse(message);
        //         = (string)json["OutputDir"];
        //        SourceName = (string)json["SourceName"];
        //        ThumbnailSize = ((string)json["ThumbnailSize"]);
        //        LogName = (string)json["LogName"];

        //        public void SendCommandToService(CommandRecievedEventArgs command)
        //        {
        //            m_client.Write(command.ToJson());
        //        }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Image Count: ")]
        public string ImageCount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Setvice Status")]
        public string ServiceStatus { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "studentsList: ")]
        public ObservableCollection<Student> StudentsList { get; set; }

    }
}

      

    


