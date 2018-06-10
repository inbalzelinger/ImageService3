using communication.Client;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Threading;
using System.Windows.Data;

namespace ContosoUniversity.Models
{
    public class RemoveHandlerModel
    {
        private IClient m_client;
        


        public RemoveHandlerModel(String handlerToRemove)
        {
            Handler = handlerToRemove;
            try
            {
                this.m_client = Client.ClientInstance;
                this.m_client.OnMessageRecived += GetMessageFromClient;
                //BindingOperations.EnableCollectionSynchronization(Handlers, new object());
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
                           }
            catch
            {
                Console.Write("Error");
            }
        }



        public void RemoveHandler()
        {
            Debug.WriteLine("in onRemove");
            if (Handler == null)
                 return;
            Debug.WriteLine("in onRemove:", Handler);
            List<string> args = new List<string>();
            args.Add(Handler);
            SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args.ToArray(), null));
        }


        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            m_client.Write(command.ToJson());
        }


        private void GetMessageFromClient(object sender, string message)
        {
           
            if (message.Contains("Close "))
            {
                string removedHandler = message.Substring(("Close ".Length));
                //m_handler.Remove(removedHandler);
            }
            else
            {
                Debug.WriteLine("Config model ignored message = " + message);
            }
        }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handler")]
        public string Handler { get; set; }
    }
}

      

    


