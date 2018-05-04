using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Gui.ViewModel
{
    class SettingsVM : INotifyPropertyChanged
    {

        #region Notify Changed
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var command = this.SubmitCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

    }
}
