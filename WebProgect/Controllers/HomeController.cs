using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProgect.Models;

namespace WebProgect.Controllers
{
    public class HomeController : Controller
    {
        static ConfigModel configModel;
        static LogsWebModel logsWebModel;
        static ImageWebModel imageWebModel;                                             

        public HomeController()
        {
            configModel = new ConfigModel();
            logsWebModel = new LogsWebModel();
            imageWebModel = new ImageWebModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Config()
        {
            return View(configModel);
        }

        public ActionResult ImageWeb()
        {
            return View(imageWebModel);
        }

        public ActionResult Logs()
        {
            return View(logsWebModel);
        }
    }
}