using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communication
{
    class TCPClientHandler
    {
        #region members
        private ITelnetClient m_client;
        private StreamWriter m_writer;
        private StreamReader m_reader;
        private CancellationTokenSource m_cancellationTokenSource;
        private Stream m_stream;
        #endregion




        public TCPClientHandler(ITelnetClient client)
        {
            m_client = client;
            //m_stream = client.getStrem;
            m_reader = new StreamReader(m_stream, Encoding.ASCII);
            m_writer = new StreamWriter(m_stream, Encoding.ASCII);
            m_cancellationTokenSource = new CancellationTokenSource();
        }

    }
}
