using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved; // The event that notifies about a new Command being recieved
        public event EventHandler<CommandRecievedEventArgs> CloseService;
        #endregion

        public ImageServer(IImageController controller, ILoggingService loggingService)
        {
            this.m_controller = controller;
            this.m_logging = loggingService;
            string[] directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
            foreach (string directory in directories)
            {
                try
                {
                    IDirectoryHandler handler = new DirectoyHandler(m_controller, m_logging , directory);
                    CloseService += handler.OnCloseSevice;
                    CommandRecieved += handler.OnCommandRecieved;
                    handler.StartHandleDirectory(directory);
                    m_logging.Log("Handler created for " + directory, Logging.Modal.MessageTypeEnum.INFO);
                }
                catch (Exception e)
                {
                    m_logging.Log("faild to create handler for: " + directory, Logging.Modal.MessageTypeEnum.FAIL);
                }
            }
        }


        public void OnCloseSevice()
        {
            try
            {
                m_logging.Log("server->on flose service", Logging.Modal.MessageTypeEnum.INFO);
                CloseService?.Invoke(this, null);
            }
            catch (Exception ex)
            {
                this.m_logging.Log("server->on flose service Exception: " + ex.ToString(), Logging.Modal.MessageTypeEnum.FAIL);
            }
        }
    }



}









