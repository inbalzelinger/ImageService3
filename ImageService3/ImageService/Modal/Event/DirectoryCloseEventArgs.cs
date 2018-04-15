using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class DirectoryCloseEventArgs : EventArgs
    {
        public string DirectoryPath { get; set; }

        public string Message { get; set; }             // The Message That goes to the logger
          ///</summary>
         //  constructor
        /// </summary>
        /// <param name="message">The Message That goes to the logger</param name
        /// <param name="dirpath">path of requasted dir</param name
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

    }
}
