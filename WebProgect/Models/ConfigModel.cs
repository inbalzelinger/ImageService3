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
using System.Threading;
using System.Windows.Data;

namespace ContosoUniversity.Models
{
    public class ConfigModel
    {
        private IClient m_client;
        private bool finish;
        //private  m_handler;


        public ConfigModel()
        {
            OutputDirectory = "";
            LogName = "";
            SourceName = "";
            ThumbnailSize = "";
            try
            {
                this.m_client = Client.ClientInstance;

                this.m_client.OnMessageRecived += GetMessageFromClient;

                Handlers = new ObservableCollection<string>();
                //BindingOperations.EnableCollectionSynchronization(Handlers, new object());
                finish = false;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
                SpinWait.SpinUntil(() => finish);
            }
            catch
            {
                Console.Write("Error");
            }
        }

        private void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("Config "))
            {
                Debug.WriteLine("Working on config...");
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                JObject json = JObject.Parse(message);
                OutputDirectory = (string)json["OutputDir"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = ((string)json["ThumbnailSize"]);
                LogName = (string)json["LogName"];
                string[] handlersArray = ((string)json["Handler"]).Split(';');
                foreach (string handler in handlersArray)
                {
                    Handlers.Add(handler);
                }
                finish = true;
                Debug.WriteLine("Done!");
            }
            else if (message.Contains("Close "))
            {
                string removedHandler = message.Substring(("Close ".Length));
                //m_handler.Remove(removedHandler);
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


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize: ")]
        public string ThumbnailSize { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputDirectory")]
        public string OutputDirectory { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName: ")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName: ")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers: ")]
        public ObservableCollection<string> Handlers { get; set; }

    }
}

      

    


