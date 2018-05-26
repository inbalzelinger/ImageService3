using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ImageService3;
using Gui.models;
using ImageService.Logging.Modal;
using System.Collections.ObjectModel;

namespace Gui.ViewModel
{
    class LogsVM : INotifyPropertyChanged

    {
        private LogsModel m_logsModel;
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<MessageRecievedEventArgs> logs;

        #region properties
        public ObservableCollection<MessageRecievedEventArgs> VM_Logs
        {
            get
            {
                return m_logsModel.LogsList;
            }
            set
            {
                logs = value;
            }
        }
        #endregion

    
        public LogsVM()
        {
            this.m_logsModel = new LogsModel();

            m_logsModel.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_Logs");
                };
            m_logsModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propname)
        {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

    }
}







    
