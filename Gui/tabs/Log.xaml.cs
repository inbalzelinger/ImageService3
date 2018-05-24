using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gui.ViewModel;

namespace Gui.tabs
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log : UserControl
    {
        LogsVM m_logsVM;

        public Log()
        {
            InitializeComponent();
            m_logsVM = new LogsVM();
            this.DataContext = m_logsVM;
        }
    }
}
