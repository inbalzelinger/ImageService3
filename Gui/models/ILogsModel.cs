using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Gui.models
{
    interface ILogsModel: INotifyPropertyChanged
    
    {
        String ServerIP { get; set; }
        int ServerPort { get; set; }
      
    }
}
