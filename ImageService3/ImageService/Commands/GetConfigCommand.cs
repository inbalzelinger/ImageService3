using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Modal;


namespace ImageService3.ImageService.Commands
{
    public class GetConfigCommand : ICommand
    {

        public string Execute(string[] args, out bool result)
        {
            string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
            string loggerName = ConfigurationManager.AppSettings.Get("LogName");
            string ThumbnailSize = ConfigurationManager.AppSettings.Get("ThumbnailSize");
            string OutputDir = ConfigurationManager.AppSettings.Get("OutputDir");
            string Handler = ConfigurationManager.AppSettings.Get("Handler");



        }
    }
}
