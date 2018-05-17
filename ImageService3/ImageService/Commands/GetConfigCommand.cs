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
        private IImageServiceModal m_modal;


        public GetConfigCommand(IImageServiceModal modal)
        {
            this.m_modal = modal;
        }

        public string Execute(string[] args, out bool result)
        {
            try
            {
                string s = this.m_modal.GetConfigFile(out result);
                return s;
            }
            catch (Exception e)
            {
                result = false;
                return null;

            }

        }
    }
}
