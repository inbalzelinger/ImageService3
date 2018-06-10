﻿using ContosoUniversity.Models;
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
        static LogsWebModel logsWebModel = new LogsWebModel();
        static PhotosWebModel photosWebModel;
        static ImageWebModel imageWebModel;
   


        public HomeController()
        {
            configModel = new ConfigModel();
            photosWebModel = new PhotosWebModel(configModel.OutputDirectory);
            imageWebModel = new ImageWebModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Co()
        {
            ViewBag.Message = "Your contact page.";

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

        public ActionResult Photos()
        {
            return View(photosWebModel);
        }
    }
}