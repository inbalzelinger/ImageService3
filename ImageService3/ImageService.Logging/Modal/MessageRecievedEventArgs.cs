using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    public class MessageRecievedEventArgs : EventArgs
    {

        private MessageTypeEnum m_status;
        private string m_message;

        public MessageTypeEnum Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }



        public MessageRecievedEventArgs(MessageTypeEnum status, string message)
        {
            this.m_status = status;
            this.m_message = message;
        }
        /*
        public static MessageRecievedEventArgs FromJson(string s)
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(s);
                int messageType = (int)jObject["Status"];
                string message = (string)jObject["Message"];
                return new MessageRecievedEventArgs((MessageTypeEnum)messageType, message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        */
        public string ToJson()
        {
            //One string with no new lines.
            return JsonConvert.SerializeObject(this).Replace(Environment.NewLine, " ");
        }

    }
    }

