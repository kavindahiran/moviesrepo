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
                return RedirectToAction("dashboardshop", "LoadShops");
            }
            else
            {
                return RedirectToAction("loginfailed", "errors");
            }
            
         
        }

        public ActionResult dashboardshop()
        {
            int shopid =Convert.ToInt32(Session["shopid"]);
            moviedetailsdb1 db = new moviedetailsdb1();
            List<Landmark121> l = new List<Landmark121>();
            List<Movie_Item> mov = new List<Movie_Item>();
            shopandmovieModel model = new shopandmovieModel();
            model.ms = db.movieshops.Where(x => x.shopid == shopid).SingleOrDefault();
            
            model.movie_id = db.Landmark121.Where(x => x.shopid == shopid).Count();
            model.moviecount=db.Landmark121.Where(x => x.shopid == shopid).GroupBy(x=>x.MovieId).Count();
            model.coupencount = db.coupentbls.Where(x => x.shopid == shopid).Count();
            //model.land = (from lnd in l
            //              join movie in mov on lnd.MovieId equals movie.movie_id
            //              where lnd.shopid == shopid
            //              select new shopandmovieModel { shopid = lnd.shopid }).ToList();

            model.land = db.Landmark121.Where(x=>x.shopid==shopid).ToList();

            return View(model);  
        }

        public ActionResult generatecoupen()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<movieshop> sh = db.movieshops.ToList();
            ViewBag.movielists = new SelectList(sh, "shopid", "shopname");
            return View();
        }
        [HttpPost]
        public ActionResult getcoupen(movieshop ms)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            customcoupenmodel cou = new customcoupenmodel();
            movieshop sh = db.movieshops.Where(x=>x.shopid==ms.shopid).FirstOrDefault();
            cou.shopname = sh.shopname;
            cou.ownername = sh.ownername;
            cou.mobileno = sh.mobileno;
            cou.shopimage = sh.shopimage;
            cou.address1 = sh.address1;
            cou.city = sh.city;
            cou.state = sh.state;

            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                s = String.Concat(s, random.Next(10).ToString());
            }
            int userid =Convert.ToInt32(Session["id"]);
            string username = Session["regname"].ToString();
            coupentbl ctb = new coupentbl();
            ctb.coupenNo = s;
            ctb.shopid = ms.shopid;
            ctb.genDate = DateTime.Now;
            ctb.discount = (15 * 10) / 10;
            ctb.userid = userid;
            db.coupentbls.Add(ctb);
            db.SaveChanges();

            coupentbl cp= db.coupentbls.Where(x => x.shopid == ms.shopid && x.userid==userid).OrderByDescending(x=>x.genDate).FirstOrDefault();

            cou.discount = cp.discount;
            cou.genDate = cp.genDate;
            cou.coupenNo = cp.coupenNo;
            cou.username = username;

            return View(cou);
        }

        public ActionResult downloadsoftware()
        {
            return View();
        }

        public ActionResult homepage()
        {
            return View();
        }

    }
}