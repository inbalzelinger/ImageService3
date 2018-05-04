using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ImageService3;

namespace Gui.ViewModel
{
    class LogsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ImageService3.ImageService3 m_modal;



        public LogsVM(ImageService3.ImageService3 modal)
        {
            
            this.m_modal = modal;
        }

    
        protected void onPropertyChanged(String name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
