using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ImageService.Logging.Modal;
using System.Collections.ObjectModel;
using ImageService.Modal;

namespace Gui.models
{
    interface ILogsModel : INotifyPropertyChanged
    {
        #region properties

        ObservableCollection<MessageRecievedEventArgs> LogsList { get; set; }
      //  string WhichMessageType { get; }

        #endregion

        void SendCommandToService(CommandRecievedEventArgs command);
    }
}
