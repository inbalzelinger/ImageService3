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
using System.Text.RegularExpressions;

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

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;  // The Event That Notifies that the Directory is being closed


        public DirectoyHandler(IImageController m_controller , ILoggingService m_logging , string m_path)
        {
            this.m_controller = m_controller;
            this.m_logging = m_logging;
            this.m_path = m_path;
            this.m_dirWatcher = new FileSystemWatcher();

        }



        public void StartHandleDirectory(string dirPath)
        {
            this.m_logging.Log("start listning to: " + dirPath, MessageTypeEnum.INFO);
            // 

            string[] allFilesInDir = Directory.GetFiles(dirPath);
            foreach(string file in allFilesInDir)
            {
                this.m_logging.Log("StartHandleDirectory: " + file , MessageTypeEnum.INFO);
                string ext = Path.GetExtension(file);
                if(ext == ".jpg" || ext == ".png"|| ext == ".gif"|| ext == ".bmp")
                {
                    this.m_logging.Log("new file was found!!: " + file, MessageTypeEnum.INFO);


                    string[] args = { "file" };
                    CommandRecievedEventArgs e = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, 
                        args, file);

                    this.OnCommandRecieved(this, e);
                }
            }
        }



        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
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
    }
}
