using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Gui.comunication;

namespace Gui.models
{
    class SettingsModel : ISettingsModel
    {
        private ITelnetClient m_client;
        private string m_outputDirectory;
        private string m_surceName;
        private string m_logName;
        private int m_thubnailSize;


        public event PropertyChangedEventHandler PropertyChanged;


        public SettingsModel(ITelnetClient client)
        {
            m_client = client;
        }


        public List<string> GetHandlers()
        {
            throw new NotImplementedException();
        }

        public string GetLogName()
        {
            throw new NotImplementedException();
        }

        public string GetOutputDirectory()
        {
            throw new NotImplementedException();
        }

        public string GetSourceName()
        {
            throw new NotImplementedException();
        }

        public int GetYhumbnailSize()
        {
            throw new NotImplementedException();
        }
    }
}
