using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    public interface IDirectoryHandler
    {
        //add the close command.
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed
        void StartHandleDirectory(string dirPath);             // The Function Recieves the directory to Handle
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);     // The Event that will be activated upon new Command
        void OnCloseSevice(object sender, CommandRecievedEventArgs e);    

    }
}
