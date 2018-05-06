using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Gui.models
{
    interface ISettingsModel : INotifyPropertyChanged
    {


        #region properties
        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        int ThumbnailSize { get; set; }
        //change to obvervable colection.
        List<string> Handlers { get; set; }
        #endregion


        //check the client thing. this should be connected to the service somehow.
        // if the server will get the command get appconfig it will return all the fields.

        void Connect(string IP ,int port);
        void Disconnect();
        //first we want to take all the inpormation of the appconfig.
        void Start();
    }
}
