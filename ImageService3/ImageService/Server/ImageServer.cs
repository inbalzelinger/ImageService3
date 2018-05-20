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
using System.Threading;
using System.IO;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;

        private string[] directories;

        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved; // The event that notifies about a new Command being recieved

        public event EventHandler<CommandRecievedEventArgs> CloseService;
        #endregion
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller">image controller</param>
        /// <param name="loggingService">logger</param>
        public ImageServer(IImageController controller, ILoggingService loggingService)
        {
            this.m_controller = controller;
            this.m_logging = loggingService;
            directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
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

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller">image controller</param>
        /// <param name="loggingService">logger</param>
        /// 
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




        //private static Mutex clientsMutex = new Mutex();

        //public void HandleClient(TcpClient client)
        //{        
        //    new Task(() =>
        //    {
        //        NetworkStream stream = client.GetStream();
        //        BinaryReader reader = new BinaryReader(stream);
        //        BinaryWriter writer = new BinaryWriter(stream);
        //        while (client.Connected)
        //        {
        //            string commandLine = reader.ReadString();
        //            if (commandLine == null)
        //                continue;
        //            CommandRecievedEventArgs crea = CommandRecievedEventArgs.FromJson(commandLine);
        //            bool result;
        //            string res = this.m_controller.ExecuteCommand(crea.CommandID, crea.Args, out result);
        //            clientsMutex.WaitOne();
        //            writer.Write(res);
        //            clientsMutex.ReleaseMutex();
        //            res = string.Empty;
        //        }
        //    }).Start();
        //}
        
    }

}
