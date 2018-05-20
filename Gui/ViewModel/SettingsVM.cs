using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Gui.models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Gui.ViewModel
{
    class SettingsVM : INotifyPropertyChanged
    {
        #region members
        private ISettingsModel m_settingsModel;
        private string m_handlerToRemove;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;


        #region properties

        public string IsConnect
        {
            get
            {
                return this.m_settingsModel.IsConnect;
            }
        }


        public string HandlerToRemove
        {
            get
            {
                return this.m_handlerToRemove;
            }
            set
            {
                this.m_handlerToRemove = value;
            }
        }



        public string VM_OutputDirectory
        {
            get
            {
                return m_settingsModel.OutputDirectory;
            }
            set
            {
                this.VM_OutputDirectory = value;
            }
        }

        public string VM_SourceName
        {
            get
            {
                return m_settingsModel.SourceName;
            }
            set
            {
                this.VM_SourceName = value;
            }
        }
        public string VM_LogName
        {
            get
            {
                return m_settingsModel.LogName;
            }
            set
            {
               // this.VM_LogName = value;
            }
        }

        public int VM_ThumbnailSize
        {
            get
            {
                return m_settingsModel.ThumbnailSize;
            }
            set
            {
                this.VM_ThumbnailSize = value;
            }
        }

         public ObservableCollection<string> VM_Handlers
        {
            get
            {
                return m_settingsModel.Handlers;
            }
            set
            {
                this.VM_Handlers = value;
            }
        }
        #endregion

        public SettingsVM()
        {
            this.m_settingsModel = new SettingsModel();
            m_settingsModel.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
            m_settingsModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
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
