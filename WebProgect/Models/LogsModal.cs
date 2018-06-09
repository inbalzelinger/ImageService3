using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using communication.Client;
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace WebProgect.Models
{
    public class LogsModal
    {

        private IClient m_client;
        private ObservableCollection<MessageRecievedEventArgs> m_logs;
        public event PropertyChangedEventHandler PropertyChanged;


        #region properties
        public ObservableCollection<MessageRecievedEventArgs> LogsList

        {
            get
            {
                return this.m_logs;
            }
            set
            {
                this.m_logs = value;
            }
        }
        #endregion

        public LogsModel()
        {
            try
            {

                this.m_client = Client.ClientInstance;
                this.m_client.OnMessageRecived += GetMessageFromClient;
                LogsList = new ObservableCollection<MessageRecievedEventArgs>();
                BindingOperations.EnableCollectionSynchronization(LogsList, new object());
                LogsList.CollectionChanged += (s, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogsList"));
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void GetMessageFromClient(object sender, string message)
        {
            if (message[0] != '{') return;
            CommandRecievedEventArgs commandRecieved = CommandRecievedEventArgs.FromJson(message);
            if (commandRecieved.CommandID == (int)CommandEnum.LogCommand)
            {
                // if (message.Contains("GetLog "))
                //if (!message.Contains("Config "))


                ObservableCollection<MessageRecievedEventArgs> tempList = new ObservableCollection<MessageRecievedEventArgs>();

                // int i = message.IndexOf(" ") + 1;
                //message = message.Substring(i);

                string[] logsStrings = commandRecieved.Args[0].Split(';');
                foreach (string s in logsStrings)
                {
                    if (s.Contains("Status") && s.Contains("Message"))
                    {
                        try
                        {
                            JObject jObject = (JObject)JsonConvert.DeserializeObject(s);
                            int messageType = (int)jObject["Status"];
                            string msg = (string)jObject["Message"];
                            MessageRecievedEventArgs messageRecieved = new MessageRecievedEventArgs((MessageTypeEnum)messageType, msg);
                            LogsList.Add(messageRecieved);
                        }
                        catch (Exception e) { throw e; }

                    }
                }
                //  ObservableCollection<MessageRecievedEventArgs> tempList = new ObservableCollection<MessageRecievedEventArgs>();
                //MessageRecievedEventArgs messageRecieved = new MessageRecievedEventArgs(MessageTypeEnum.FAIL, message);
                //tempList.Add(messageRecieved);
                //tempList.Add(new MessageRecievedEventArgs(MessageTypeEnum.FAIL, "example fail"));
                //tempList.Add(new MessageRecievedEventArgs(MessageTypeEnum.WARNING, "example warning"));
                Console.WriteLine("Done!");

            }

        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            m_client.Write(command.ToJson());
        }





    }
}