using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService3.ImageService.Commands;
using ImageService.Logging;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private ILoggingService m_loggingService;
        private Dictionary<int, ICommand> commands;
        ///<summary>
        ///constructor
        ///</summary>
        public ImageController(IImageServiceModal modal, ILoggingService loggingService)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            m_loggingService = loggingService;
            commands = new Dictionary<int, ICommand>();
           
            commands[(int)(CommandEnum.NewFileCommand)] = new NewFileCommand(m_modal);
            commands[(int)(CommandEnum.GetConfigCommand)] = new GetConfigCommand(m_modal);
            commands[(int)(CommandEnum.LogCommand)] = new LogCommand(m_loggingService);
        }
        ///<summary>
        ///executing the Command according to the command id in the dictinary
        ///</summary>
        /// <param name="commandID">command id</param name>
        /// <param name="args">args for the command</param name>
        /// <param name="resultSuccesful">if execute well</param name>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            Task<string[]> task = new Task<string[]>(() =>
           {
               bool resultSuccesful1;
               string msg = this.commands[commandID].Execute(args, out resultSuccesful1);
               string[] arr = { msg, resultSuccesful1.ToString() };
               return arr;
           });
            task.Start();
            if (task.Result[1] == "true")
            {
                resultSuccesful = true;
            }
            else
            {
                resultSuccesful = false;
            }
            return task.Result[0];
            
         
        }
    }

}
