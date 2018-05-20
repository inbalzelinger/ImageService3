using ImageService.Commands;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService3.ImageService.Commands
{
    class CloseHandler : ICommand
    {
        private IImageServiceModal m_modal;

        public CloseHandler(IImageServiceModal modal)
        {
            this.m_modal = modal;
        }

        public string Execute(string[] args, out bool result)
        {
            string res = "true";
            this.m_modal.CloseHandlerCommand(out result);
            return res;
        }
    }
}
