using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Gui.ViewModel
{
    interface IsettingsVM : INotifyPropertyChanged
    {

        #region properties
        string ViewOutputDirectory { get; set; }
        string ViewSourceName { get; set; }
        string ViewLogName { get; set; }
        int ViewThumbnailSize { get; set; }
        //change to obvervable colection.
        List<string> ViewHandlers { get; set; }
        #endregion

    }
}
