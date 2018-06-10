using ImageService.Commands;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService3.ImageService.Commands
{
    public class GetImageWebCommand : ICommand
    {
        private IImageServiceModal m_modal;

        public GetImageWebCommand(IImageServiceModal modal)
        {
            this.m_modal = modal;
        }

        public string Execute(string[] args, out bool result)

        {
            try
            {
                string s = this.m_modal.GetImageWebDetails(out result);
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

