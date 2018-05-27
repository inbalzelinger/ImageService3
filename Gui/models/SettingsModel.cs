using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using communication.Client;
using ImageService.Commands;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Data;

namespace Gui.models
{
    class SettingsModel : ISettingsModel
    {
        private IClient m_client;
        private string m_outputDirectory;
        private string m_surceName;
        private string m_logName;
        private int m_thubnailSize;
        ObservableCollection<string> m_handler;

        public event PropertyChangedEventHandler PropertyChanged;


        public SettingsModel()
        {
            try
            {
                this.m_client = Client.ClientInstance;

                this.m_client.OnMessageRecived += GetMessageFromClient;

                Handlers = new ObservableCollection<string>();
                BindingOperations.EnableCollectionSynchronization(Handlers, new object());

                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
            }
            catch
            {
                NotConnectedValues();
            }
        }

        public string IsConnect
        {
            get
            {
                try
                {
                    if (this.m_client.Connection == true)
                    {
                        return "#FFFFFFFF";

                    }

                }
                catch (Exception e)
                {
                    return "#FFA9A9A9";
                }
                return "#FFA9A9A9";
            }
        }

        public void NotConnectedValues()
        {
            OutputDirectory = "Not connected";
            ThumbnailSize = 1;
            SourceName = null;
            LogName = null;
            Handlers = null;
        }


        public void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("Config "))
            {
                Debug.WriteLine("Working on config...");
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                JObject json = JObject.Parse(message);
                OutputDirectory = (string)json["OutputDir"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = int.Parse((string)json["ThumbnailSize"]);
                LogName = (string)json["LogName"];
                string[] handlersArray = ((string)json["Handler"]).Split(';');
                foreach (string handler in handlersArray)
                {
                    Handlers.Add(handler);
                }
                Debug.WriteLine("Done!");
            }
            else if (message.Contains("Close "))
            {
                string removedHandler = message.Substring(("Close ".Length));
                m_handler.Remove(removedHandler);
                //string[] newHandlers = message.Split(';');
                // Handlers = new ObservableCollection<string>(newHandlers);
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



        public string OutputDirectory
        {
            get
            {
                return this.m_outputDirectory;
            }
            set
            {
                this.m_outputDirectory = value;
                this.NotifyPropertyChanged("OutputDirectory");
            }
        }


        public string SourceName
        {
            get
            {
                return this.m_surceName;
            }
            set
            {
                this.m_surceName = value;
                this.NotifyPropertyChanged("SourceName");
            }
        }


        public string LogName
        {
            get
            {
                return this.m_logName;
            }
            set
            {
                this.m_logName = value;
                this.NotifyPropertyChanged("LogName");
            }
        }

        public int ThumbnailSize
        {
            get
            {
                return this.m_thubnailSize;
            }
            set
            {
                this.m_thubnailSize = value;
                this.NotifyPropertyChanged("ThumbnailSize");
            }
        }



        public ObservableCollection<string> Handlers
        {
            get
            {
                return this.m_handler;
            }
            set
            {
                this.m_handler = value;
                this.NotifyPropertyChanged("Handlers");
            }
        }



        public void NotifyPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
