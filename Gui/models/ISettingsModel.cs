using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Gui.models
{
    interface ISettingsModel : INotifyPropertyChanged
    {

        #region properties
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        int ThumbnailSize { get; set; }
        //change to obvervable colection.
        List<string> Handlers { get; set; }
        #endregion
    }
}
