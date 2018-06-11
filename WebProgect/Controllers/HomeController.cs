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
        static ConfigModel configModel = new ConfigModel();
        static LogsWebModel logsWebModel = new LogsWebModel();
        static PhotosWebModel photosWebModel;
        static ImageWebModel imageWebModel;
        static RemoveHandlerModel removeHandlerModel;

        static ViewImage viewImage;
        static DeleteImage deleteImage;
        static ExecuteDeleteModel executeDeleteModel;



        public HomeController()
        {
            
            photosWebModel = new PhotosWebModel(configModel.OutputDirectory);
            imageWebModel = new ImageWebModel();

        }

   

        public ActionResult Config()
        {
            return View(configModel);
        }


        public ActionResult ImageWeb()
        {
            imageWebModel.Send();
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

        public ActionResult DeleteImage(string name, string thumbPath, string imagePath)
        {
            deleteImage = new DeleteImage(name, thumbPath, imagePath);
            return View(deleteImage);
        }

        public ActionResult ExecuteDelete(string imagePath, string thumbPath)
        {
            executeDeleteModel = new ExecuteDeleteModel(imagePath, thumbPath);
            executeDeleteModel.DeleteAPhoto();
            return View(executeDeleteModel);

        }

        public ActionResult ViewImage(string date, string name, string imagePath, string thumbPath)
        {
            viewImage = new ViewImage(date, name, imagePath, thumbPath);
            return View(viewImage);
        }


        public ActionResult RemoveHandler(string handlerToRemove)
        {
            removeHandlerModel = new RemoveHandlerModel(handlerToRemove);

            return View(removeHandlerModel);
        }

        public ActionResult DeleteHandlerForSure(string handlerToRemove)
        {
            removeHandlerModel.RemoveHandler();
            try
            {
                configModel.HandlerToramove(handlerToRemove);

            }
            catch
            {
                ;
            }
            return RedirectToAction("Config", "Home");
            
        }
       
    }
}