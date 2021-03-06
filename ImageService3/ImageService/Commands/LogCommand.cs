﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;

namespace ImageService3.ImageService.Commands
{
    class LogCommand : ICommand
    {

        //private IImageServiceModal m_modal;
        private ILoggingService m_logger;
        public LogCommand(ILoggingService loggingService)
        {
            //this.m_modal = modal;
            this.m_logger = loggingService;
        }

        public string Execute(string[] args, out bool result)
        {
            /* try
             {
                 string s = this.m_modal.GetAllLogs(out result);
                 return s;
             }
             catch (Exception e)
             {
                 result = false;
                 return null;
             }
         }*/
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (MessageRecievedEventArgs item in m_logger.LogsList)
                {
                    sb.Append(item.ToJson() + " ; ");
                }
                result = true;
                string[] arguments = new string[1];
                arguments[0] = sb.ToString();
                CommandRecievedEventArgs c = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, arguments, null);
                return c.ToJson();
            }
            catch (Exception e)
            {
                result = false;
                return e.ToString();
            }

        }
    }
}

