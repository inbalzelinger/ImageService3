using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using communication;
using communication.server;
using ImageService.Controller;
using ImageService.Modal;

namespace ImageService3.ImageService.Server
{
    class GUIServer
    {
        private IServer m_server;
        private IImageController m_controller;

        private static GUIServer m_instance = null;



        public static GUIServer Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new GUIServer();
                    return m_instance;
                }
                else
                {
                    return m_instance;
                }
            }
            set
            {
                ;
            }
        }



        public event EventHandler<string> OnMessageRecived
        { add { m_server.OnMessageRecived += value; } remove { m_server.OnMessageRecived -= value; } }



        private GUIServer()
        {
            m_server = new communication.server.Server();
            m_server.Start();
        }

        public void Write(string command)
        {
            m_server.Write(command);
        }
        
    }
}
