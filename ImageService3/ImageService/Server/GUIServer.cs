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

        public GUIServer(IImageController controller)
        {
            m_controller = controller;
            m_server = new communication.server.Server();
            m_server.OnMessageRecived += M_server_OnMessageRecived;
            m_server.Start();
        }



        private void M_server_OnMessageRecived(object sender, string e)
        {
            try
            {
                CommandRecievedEventArgs crea = CommandRecievedEventArgs.FromJson(e);
                bool result;
                string res = this.m_controller.ExecuteCommand(crea.CommandID, crea.Args, out result);
                IClientHandler clientHandler = (IClientHandler)sender;
                clientHandler.Write(this, res);
            }
            catch 
            {
                Debug.Write("ok!!!!!!!!!!!!!");
            }
        }
    }
}
