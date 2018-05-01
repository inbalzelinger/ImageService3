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
using System.Windows.Shapes;
using ImageService3;

namespace gui
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        #region members
        private string m_outputDir;
        private string m_sourceName;
        private string m_logName;
        private string m_thumbnailSize;
        private List<string> handlers;
        #endregion

        #region proprties

        #endregion


        public Settings()
        {
            InitializeComponent();
            handlers = new List<string>();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
