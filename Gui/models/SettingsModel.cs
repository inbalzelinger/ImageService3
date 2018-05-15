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



namespace Gui.models
{
    class SettingsModel : ISettingsModel
    {
        private IClient m_client;
        private string m_outputDirectory;
        private string m_surceName;
        private string m_logName;
        private int m_thubnailSize;
        private List<string> m_handlers; 
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsModel()
        {
            try
            {
                this.m_client = Client.ClientInstance;
                this.m_client.MessageRecived += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
            }
            catch (Exception e)
            {
                NotConnectedValues();
            }
        }



        public void NotConnectedValues()
        {
            OutputDir = "Not connected to server!";
            SourceName = null;
            ThumbnailSize = 0;
            LogName = null;
            Handlers = null;
        }
        public void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("Config "))
            {
                Console.WriteLine("Working on config...");
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                JObject json = JObject.Parse(message);
                OutputDir = (string)json["OutputDir"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = int.Parse((string)json["ThumbnailSize"]);
                LogName = (string)json["LogName"];
                string[] handlersArray = ((string)json["Handler"]).Split(';');
                Handlers = new ObservableCollection<string>(handlersArray);

                Console.WriteLine("Done!");
            }
            else if (message.Contains("Close "))
            {
                string[] newHandlers = message.Split(';');
                Handlers = new ObservableCollection<string>(newHandlers);
            }
            else
            {
                Console.WriteLine("Config model ignored message = " + message);
            }
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }












        public SettingsModel()
        {
            this.m_client = Client.ClientInstance;




            // m_client.Write("get app config");
            string s = this.m_client.Read();
            JObject jsonObject = JObject.Parse(s);
            m_outputDirectory = (string) jsonObject["outputDirectory"];
            m_surceName = (string) jsonObject["surceName"];
            m_logName = (string) jsonObject["mlogName"];
            m_thubnailSize = (int) jsonObject["thubnailSize"];

    }

        public void Connect(string IP, int port)
        {
            this.m_client.Connent(IP, port);
        }

        public void Disconnect()
        {
            this.m_client.Disconnect();
        }

        public void Start()
        {
           // m_client.Write("get app config");
            //string s = this.m_client.Read();
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
                this.NotifyPropertyChanged("ISettingsModel.OutputDirectory");
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
                this.NotifyPropertyChanged("ISettingsModel.SourceName");
            }
        }


       public  string LogName
        {
            get
            {
                return this.m_logName;
            }
            set
            {
                this.m_logName = value;
                this.NotifyPropertyChanged("ISettingsModel.LogName");
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
                this.NotifyPropertyChanged("ISettingsModel.ThumbnailSize");
            }
        }

       public List<string> Handlers
        {
            get
            {
                return this.m_handlers;
            }
            set
            {
                this.m_handlers = value;
                this.NotifyPropertyChanged("ISettingsModel.Handlers");
            }
        }


        public void NotifyPropertyChanged(string propname)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
