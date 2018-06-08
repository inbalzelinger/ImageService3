using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Models;
using communication.Client;


namespace WebProgect.BL
{
    public interface IImageServiceProxy
    {
        bool IsAlive();
        int GetPictureCount();
        IEnumerable<Student> GetStudents();
    }

    public class ImageServiceProxy : IImageServiceProxy
    {
        private IClient m_client;

        public ImageServiceProxy()
        { 
           this.m_client = Client.ClientInstance;
           this.m_client.OnMessageRecived += GetMessageFromClient;
        }

        private void GetMessageFromClient(object sender, string e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }

        public bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public int GetPictureCount()
        {
            throw new NotImplementedException();
        }
    }
}