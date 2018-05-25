   
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImageService.Logging
{

    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        private List<MessageRecievedEventArgs> m_logsList;
        public LoggingService()
        {
            this.m_logsList = new List<MessageRecievedEventArgs>();
        }
        
        public List<MessageRecievedEventArgs> LogsList
        {  
                get
                {
               
                    return this.m_logsList;

                }
            
        }

        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));
            //add every log args to the list for the log command
            this.m_logsList.Add(new MessageRecievedEventArgs(type, message));
        }
    }
}
