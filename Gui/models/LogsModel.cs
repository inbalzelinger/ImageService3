using communication.Client;
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui.models
{
    class LogsModel : ILogsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IClient m_client;

        private ObservableCollection<MessageRecievedEventArgs> m_logs;

        public ObservableCollection<MessageRecievedEventArgs> LogsList
        {
            get
            {
                return this.m_logs;
            }
            set
            {
                this.m_logs = value;
                //this.NotifyPropertyChanged("ISettingsModel.Handlers");
            }
        }

        public LogsModel()
        {
            try
            {
                this.m_client = Client.ClientInstance;
                this.m_client.MessageRecived += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("GetLog "))
            {
             
                ObservableCollection<MessageRecievedEventArgs> tempList = new ObservableCollection<MessageRecievedEventArgs>();
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                string[] logsStrings = message.Split(';');
                foreach (string s in logsStrings)
                {
                    if (s.Contains("Status") && s.Contains("Message"))
                    {
                        try
                        {
                            MessageRecievedEventArgs messageRecieved = MessageRecievedEventArgs.FromJson(s);
                            tempList.Add(messageRecieved);
                        }
                        catch (Exception e)
                        {
                            //
                        }
                    }
                }
                logs = tempList;
                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("Logs model ignored message = " + message);
            }
        }

        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            m_client.Write(command.ToJson());
        }


    }
}
