using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Modal;

namespace ImageService3.ImageService.Commands
{
    class LogCommand : ICommand
    {

        private IImageServiceModal m_modal;

        public LogCommand(IImageServiceModal modal)
        {
            this.m_modal = modal;
        }

        public string Execute(string[] args, out bool result)
        {
            try
            {
                string s = this.m_modal.GetAllLogs(out result);
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

