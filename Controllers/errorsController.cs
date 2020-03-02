using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingApp.Controllers
{
    public class errorsController : Controller
    {
        // GET: errors
        public ActionResult usernotfound()
        {
            string notf = "Sorry!! The page that you are looking for is not avaliable or cannot be accessed";
            ViewBag.mess = notf;
            return View();
        }

        public ActionResult custom404error()
        {
            return View();
        }

        public ActionResult custom404()
        {
            string notf = "Sorry!! The page that you are looking for is not avaliable or cannot be accessed";
            ViewBag.mess = notf;
            return View();
        }

        public ActionResult loginfailed()
        {
            return View();
        }
    }
}