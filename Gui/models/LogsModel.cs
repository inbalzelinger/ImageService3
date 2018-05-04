using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;


namespace Gui.models
{
    class LogsModel: ILogsModel
    {
        private string m_serverIP;
        private int n_serverPort;
        private ObservableCollection<>

        public event PropertyChangedEventHandler PropertyChanged;
    
        string ILogsModel.ServerIP
        {
            get
            {
                return this.m_serverIP;
            }
            set
            {
                this.m_serverIP = value;
            }
        }
        int ILogsModel.ServerPort
        {
            get
            {
                return this.n_serverPort;
            }
            set
            {
                this.n_serverPort = value;
            }
        }

    }
}
