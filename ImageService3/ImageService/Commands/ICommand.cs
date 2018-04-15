using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{


    public interface ICommand
    {
         ///<summary>
        ///pass args to the m_modal to execute the command and update result
        ///</summary>
        string Execute(string[] args, out bool result);          // The Function That will Execute The 
    }
}
