using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Gui.models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
//using Microsoft.Practices.ServiceLocation;
//using Prism.Commands;
//\using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace Gui.ViewModel
{
    class SettingsVM : INotifyPropertyChanged
    {
        #region members
        private ISettingsModel m_settingsModel;
        public ICommand SubmitCommand { get; private set; }
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
                NotifyPropertyChanged("VM_HandlerToRemove");
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
                this.m_settingsModel.OutputDirectory = value;
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
                this.m_settingsModel.SourceName = value;
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
                this.m_settingsModel.LogName = value;
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
                this.m_settingsModel.ThumbnailSize = value;
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
                this.m_settingsModel.Handlers = value;
            }
        }
        #endregion

        public SettingsVM()
        {
            this.SubmitCommand = new Prism.Commands.DelegateCommand<object>(this.OnRemove, this.CanSubmit);
            this.PropertyChanged += OnPropertyUpdate;

            this.m_settingsModel = new SettingsModel();
            m_settingsModel.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
            //m_settingsModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            //{
            //    NotifyPropertyChanged(e.PropertyName);
            //};
        }

        private void OnPropertyUpdate(object sender, PropertyChangedEventArgs e)
        {
            var command = this.SubmitCommand as Prism.Commands.DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

        private void OnRemove(object obj)
        {
            Debug.WriteLine("in onRemove");
            if (HandlerToRemove == null)
                return;

            Debug.WriteLine("in onRemove:", HandlerToRemove);
            List<string> args = new List<string>();
            args.Add(HandlerToRemove);
            this.m_settingsModel.SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args.ToArray(), null));
        }

        private bool CanSubmit(object obj)
        {
            return HandlerToRemove != null;
        }

        public void NotifyPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
