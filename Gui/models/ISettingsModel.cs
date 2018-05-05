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
        string GetOutputDirectory();
        string GetSourceName();
        string GetLogName();
        int GetYhumbnailSize();
        List<string> GetHandlers();
    }
}
