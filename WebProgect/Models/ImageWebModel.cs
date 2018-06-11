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
using System.IO;
using System.Web;

namespace ContosoUniversity.Models
{
    public class ImageWebModel

    {
        private IClient m_client;
        private string m_imageCount;
        private string m_outputFolderPath;
        private string m_serviceStatus;

        public ImageWebModel(string outputFolderPath)
        {
            StudentsList = GetListFromFile();

            m_outputFolderPath = outputFolderPath;
            ServiceStatus = "Running";
            ImageCount = Count().ToString();


            try
            {
                this.m_client = Client.ClientInstance;
               
                this.m_client.OnMessageRecived += GetMessageFromClient;

               SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetImageWebCommand, null, null));
            }
            catch
            {
                m_serviceStatus = "Not Running";
                m_imageCount = "0";
                Console.Write("Error");
            }
        }


        private void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("ImageWeb"))
            {
                Debug.WriteLine("Working on Image web details...");
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                JObject json = JObject.Parse(message);
               // ImageCount = (string)json["ImageCount"];
                ServiceStatus = (string)json["ServiceStatus"];
                Debug.WriteLine("Done!");
            }
           
            else
            {
               Debug.WriteLine("Config model ignored message = " + message);
            }
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            m_client.Write(command.ToJson());
        }

        public int Count()
        {
            //string[] files = Directory.GetFiles(m_outputFolderPath, "*", SearchOption.AllDirectories);
            string[] thumbs = Directory.GetFiles(m_outputFolderPath+ "\\" + "thumbNail", "*", SearchOption.AllDirectories);
            return thumbs.Length;
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Image Count: ")]
        public string ImageCount
        {
            get; set;
        }
        

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service Status")]
        public string ServiceStatus {
             
                get { return m_serviceStatus;
                }
                set { m_serviceStatus=value;
                }
            }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "studentsList: ")]
        public List<Student> StudentsList { get; set; }


        public static List<Student> GetListFromFile()
        {
           
                List<Student> lst = new List<Student>();
                StreamReader stream = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/StudentsDetails.txt"));
                string line = stream.ReadLine();
                while (line != null)
                {
                    string[] tokens = line.Split(',');
                    lst.Add(new Student(tokens[0], tokens[1], tokens[2]));
                    line = stream.ReadLine();
                }
                return lst;
     
        }
        public void Send()
        {
            SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetImageWebCommand, null, null));
        }
    }
}

      

    


