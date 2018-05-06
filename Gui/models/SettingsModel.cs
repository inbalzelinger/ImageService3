using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Gui.comunication;

namespace Gui.models
{
    class SettingsModel : ISettingsModel
    {
        private ITelnetClient m_client;
        private string m_outputDirectory;
        private string m_surceName;
        private string m_logName;
        private int m_thubnailSize;
        private List<string> m_handlers;

        public SettingsModel(ITelnetClient client)
        {
            m_client = client;

            
        }

        string ISettingsModel.OutputDirectory
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


        string ISettingsModel.SourceName
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


        string ISettingsModel.LogName
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

        int ISettingsModel.ThumbnailSize
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

        List<string> ISettingsModel.Handlers
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propname)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
