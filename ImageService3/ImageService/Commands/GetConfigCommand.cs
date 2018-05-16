using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Modal;
using Newtonsoft.Json.Linq;

namespace ImageService3.ImageService.Commands
{
    public class GetConfigCommand : ICommand
    {
        public GetConfigCommand()
        {
            ;
        }

        public string Execute(string[] args, out bool result)
        {
            try
            {
                JObject j = new JObject();
                j["eventSourceName"] = ConfigurationManager.AppSettings["SourceName"];
                j["ThumbnailSize"] = ConfigurationManager.AppSettings["ThumbnailSize"];
                j["loggerName"] = ConfigurationManager.AppSettings["LogName"];
                j["Handler"] = ConfigurationManager.AppSettings["Handler"];
                j["OutputDir"] = ConfigurationManager.AppSettings["OutputDir"];
                result = true;
                string ret = "Config " + j.ToString().Replace(Environment.NewLine, " ");
                return ret;
            }
            catch (Exception e)
            {
                result = false;
                return e.ToString();
            }
           
   
        }
    }
}
