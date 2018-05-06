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
        string VM_OutputDirectory { get; set; }
        string VM_SourceName { get; set; }
        string VM_LogName { get; set; }
        int VM_ThumbnailSize { get; set; }
        //change to obvervable colection.
        List<string> VM_Handlers { get; set; }
        #endregion

    }
}
