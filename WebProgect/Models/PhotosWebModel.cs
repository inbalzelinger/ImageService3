using communication.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;

namespace WebProgect.Models
{
    public class PhotosWebModel
    {
        private IClient m_client;
        private static Regex r = new Regex(":");


        public string OutputFolder { get; set; }
        public string[] PicPaths { get; set; }

        public List<Dictionary<string, string>> TthubToPic;

        public PhotosWebModel(string outputFolder)
        {
            try { 
            this.m_client = Client.ClientInstance;
            TthubToPic = new List<Dictionary<string, string>>();
            this.OutputFolder = outputFolder;
            string thubnailDir = Path.Combine(OutputFolder, "thumbNail");
            PicPaths = Directory.GetFiles(thubnailDir, "*", SearchOption.AllDirectories);

            string tmp;
            foreach (string s in PicPaths)
            {
                tmp = s.Replace(thubnailDir, OutputFolder);
                DateTime dateTime;
                string imageName = Path.GetFileName(s);
                try
                {
                    dateTime = GetDateTakenFromImage(s);
                }
                catch (Exception e)
                {
                    dateTime = File.GetCreationTime(s);
                }
                string year = dateTime.Year.ToString();
                string month = dateTime.Month.ToString();
                string date = dateTime.ToString("MM:dd:yyyy");


                TthubToPic.Add(new Dictionary<string, string>
                {
                    { "thumb", s }, { "pic" , tmp}, {"name" ,imageName}, {"date", date}
                 });
            }
            }
            catch
            {
                TthubToPic = new List<Dictionary<string, string>>();
                this.OutputFolder = outputFolder;
                PicPaths = new string[0];
            
            }
        }
    
            public static DateTime GetDateTakenFromImage(string path)
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            }
    }
}