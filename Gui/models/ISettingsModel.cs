using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Gui.models
{
    public interface ISettingsModel : INotifyPropertyChanged
    {
        #region properties
        string OutputDirectory { get; set; }
         string SourceName { get; set; }
         string LogName { get; set; }
         int ThumbnailSize { get; set; }
        ObservableCollection<string> Handlers { get; set; }

        #endregion

        //check the client thing. this should be connected to the service somehow.
        // if the server will get the command get appconfig it will return all the fields.
    }
}
