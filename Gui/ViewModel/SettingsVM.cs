using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Gui.models;

namespace Gui.ViewModel
{
    class SettingsVM : IsettingsVM
    {

        private ISettingsModel m_settingsModel;

        public SettingsVM(ISettingsModel settingsModel)
        {
            this.m_settingsModel = settingsModel;
            m_settingsModel.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }


        public string VM_OutputDirectory
        {
            get
            {
                return m_settingsModel.OutputDirectory;
            }
            set
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
        }

        public List<string> VM_Handlers
        {
            get
            {
                return m_settingsModel.Handlers;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propname)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
