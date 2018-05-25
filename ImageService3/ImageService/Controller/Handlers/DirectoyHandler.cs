using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Server;
using System.Text.RegularExpressions;
using ImageService3.ImageService.Server;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;    // The Path of directory
        #endregion
          ///</summary>
         ///create folders according  to date if
        ///needed and add the image in path
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;  // The Event That Notifies that the Directory is being closed

          ///</summary>
         ///constructor
        /// </summary>
        /// <param name="m_controller">
        public DirectoyHandler(IImageController m_controller , ILoggingService m_logging , string dirPath)
        {
            //this.m_path = dirPath;
            this.m_controller = m_controller;
            this.m_logging = m_logging;
            

        }


          ///</summary>
         //  Recieves the directory to Handle and start listen
        /// </summary>
        /// <param name="dirPath">directory to Handle</param name>
        public void StartHandleDirectory(string dirPath)
        {
            this.m_path = dirPath;
            this.m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.Path = dirPath;
            m_dirWatcher.Created += OnNewFileCreated;
            m_dirWatcher.EnableRaisingEvents = true;

        }
          ///</summary>
         // The Event that will be activated upon creation of a file
        /// </summary>
        /// <param name="sender">the notifyer</param name>
        ///<pararm name="e">arguments of the command</param name>
        private void OnNewFileCreated(object sender, FileSystemEventArgs e)
        {
            string[] args = new string[] { e.FullPath };
            bool result;
            string msg = m_controller.ExecuteCommand((int)CommandEnum.NewFileCommand, args, out result);
            m_logging.Log(msg, MessageTypeEnum.INFO);
        }


        
         ///</summary>
         // The Event that will be activated upon new Command
        /// </summary>
        /// <param name="sender">the notifyer</param name>
        ///<pararm name="e">arguments of the command</param name>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;

            if(e.CommandID == (int)CommandEnum.CloseCommand)
            {
                if (e.RequestDirPath.Equals(this.m_path))
                {
                    this.OnCloseSevice(this, e);
                    GUIServer.Instance.Write("Close " + m_path);
                }
                return;
            }

            this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            if (result)
            {
                m_logging.Log("OnCommandRecieve:  succesfuly add the file",MessageTypeEnum.INFO);
            }
            else
            {
                m_logging.Log("OnCommandRecieve:  faild to add the file",MessageTypeEnum.FAIL);
            }
        }

          ///</summary>
         ///the event that will be activated when service needs to be close
        ///// </summary>
        /// <param name="sender">the notifyer</param name>
        ///<pararm name="e">arguments of the command</param name>
        public void OnCloseSevice(object sender, CommandRecievedEventArgs e)
        {
            m_dirWatcher.EnableRaisingEvents = false;
            ImageServer server = (ImageServer)sender;
            m_dirWatcher.Dispose();
            m_logging.Log("Handler closed " + m_path, MessageTypeEnum.INFO);
            server.CloseService -= OnCommandRecieved;
        }
    }
}
