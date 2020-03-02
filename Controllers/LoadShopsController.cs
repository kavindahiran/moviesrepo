using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RatingApp.Models;
using System.Data.Entity;
using System.IO;

namespace RatingApp.Controllers
{
    public class LoadShopsController : Controller
    {
        // GET: LoadShops
        public ActionResult Index()
        {
          /*  moviedetailsdb1 db = new moviedetailsdb1();
            List<movieshop> ms = db.movieshops.ToList();*/
            return View();
        }


        public ActionResult registershop(movieshop sh, string dateC, string txt1)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            movieshop mvc = new movieshop();
            string fileName = Path.GetFileNameWithoutExtension(sh.ImageFile.FileName);
            string extension = Path.GetExtension(sh.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            mvc.shopimage = "~/shopimages/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/shopimages/"), fileName);
           
           
            sh.ImageFile.SaveAs(fileName);



         





            mvc.registredDate=DateTime.Now;
            mvc.shopname = sh.shopname;
            mvc.ownername = sh.ownername;
            mvc.address1 = sh.address1;
            mvc.address2 = sh.address2;
            mvc.landno = sh.landno;
            mvc.mobileno = sh.mobileno;
            mvc.fax = sh.fax;
            mvc.country = sh.country;
            mvc.state = sh.state;
            mvc.city = sh.city;
            mvc.zipcode = sh.zipcode;
            mvc.establisheddate =Convert.ToDateTime(dateC);
            mvc.businessinquery = txt1;
            db.movieshops.Add(mvc);
            db.SaveChanges();
            return RedirectToAction("selectTime", "Booktickets", new { id = sh.shopid });
       
        }

        public ActionResult shoplogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult loginS(string username, string pass)
        {
            moviedetailsdb1 db = new moviedetailsdb1();

            movieshop sh = db.movieshops.FirstOrDefault(x => x.shopname == username && x.passW == pass);
           
            if (sh != null)
            {
                Session["shopid"] = sh.shopid;
                Session["shopname"] = sh.shopname;
                Session["ownername"] = sh.ownername;
                //Session["roleid"] = emp.roleid;
                return RedirectToAction("registershops", "Booktickets");
            }
            else
            {
                return RedirectToAction("loginfailed", "errors");
            }
            
         
        }

        public ActionResult dashboardshop()
        {
            return View();
        }

    }
}