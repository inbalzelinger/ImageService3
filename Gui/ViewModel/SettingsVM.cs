using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Gui.models;

namespace Gui.ViewModel
{
    class SettingsVM : INotifyPropertyChanged
    {

        private ISettingsModel m_settingsModel;
        // public string VM_OutputDirectory { get; set; }
        //  public string VM_SourceName { get; set; }
        // public string VM_LogName { get; set; }
        // public int VM_ThumbnailSize { get; set; }
        //change to obvervable colection.
        // public List<string> VM_Handlers { get; set; }


        #region properties


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

        public List<string> VM_Handlers
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
            this.VM_LogName = "yyyyy";
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
