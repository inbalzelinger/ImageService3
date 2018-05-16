using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ImageService.Modal
{
    public class CommandRecievedEventArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }
        public string RequestDirPath { get; set; }  // The Request Directory
        ///</summary>
         //  constructor
        /// </summary>
        /// <param name="id">command id</param name
        /// <param name="args">args for the command</param name
        /// <param name="path">path of requasted dir</param name
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }


        public string ToJson()
        {
            JObject jStr = new JObject();
            jStr["CommandID"] = CommandID;
            jStr["RequestDirPath"] = RequestDirPath;
            jStr["Args"] = new JArray(Args);
            return jStr.ToString().Replace(Environment.NewLine, " ");
        }
        public static CommandRecievedEventArgs FromJson(string jStr)
        {
            try
            {
                JObject jObject = JObject.Parse(jStr);
                int id = (int)jObject["CommandID"];
                JArray args = (JArray)jObject["Args"];
                string[] argsArr = args.Select(c => (string)c).ToArray();
                string path = (string)jObject["RequestDirPath"];
                return new CommandRecievedEventArgs(id, argsArr, path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}
