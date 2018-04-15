using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;

         ///<summary>
        ///constructor
        ///</summary>
        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }

         ///<summary>
        ///pass args to the m_modal to execute the command and update result
        ///</summary>
        public string Execute(string[] args, out bool result)
        {
            try
            {
                if (args.Length == 0)
                {
                    throw new Exception("there were no args");
                }
                else if (File.Exists(args[0]))
                {
                    // The String Will Return the New Path if result = true, and will return the error message
                    return this.m_modal.AddFile(args[0], out result);
                }
                result = true;
                return args[0].ToString();
            } catch(Exception e)
            {
                result = true;
                return e.ToString();

            }

        }       
        }
    }
