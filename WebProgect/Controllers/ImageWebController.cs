using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgect.BL;
using ContosoUniversity.Models;

namespace WebProgect.Controllers
{
    public class ImageWebController : Controller
    {
        private IImageServiceProxy _imageServiceProxy;
        static ConfigModel configModel;
        public ImageWebController()
        {
            configModel= new ConfigModel();
        }
        /*
        // GET: ImageWeb
        public ActionResult ImageWebView()
        {
            ViewBag.ImageServiceStatus = _imageServiceProxy.IsAlive();
            ViewBag.ImageCount = _imageServiceProxy.GetPictureCount();
            IEnumerable<Student> students = _imageServiceProxy.GetStudents();

            return View(students);
        }*/

        public ActionResult Config()
        {
            return View(configModel);
        }
    }
}
