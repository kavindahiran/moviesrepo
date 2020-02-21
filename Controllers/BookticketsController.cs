using RatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingApp.Controllers
{
    public class BookticketsController : Controller
    {
        // GET: Booktickets
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult selecthall()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            showingmodelcollection model = new showingmodelcollection();
            nowshowing ok = new nowshowing();
            model.ns=db.nowshowings.ToList();

            model.hl = db.hallLocations.ToList();
            model.ht = db.halltables.ToList();

            List<nowshowing> list = db.nowshowings.ToList();

            int x =Convert.ToInt32(list[0].id);
            List<halltable> list1 = db.halltables.Where(y=>y.nowshowing.id==x).ToList();
            List<hallLocation> list2 = db.hallLocations.ToList();


            ViewBag.movielists=new SelectList(list, "id", "moviename");

            ViewBag.hallname= new SelectList(list1, "hallid", "hallname");

            ViewBag.hallLocation = new SelectList(list2, "locationid", "locationname");

            return View(model);
        }

        public ActionResult getdrop(showingmodelcollection sc, string hallname)
        {
            return View();
        }
        [HttpPost]
        public ActionResult bookhall()
        {
            return View();
        }

        public ActionResult selectTime()
        {
            return View();
        }
        public ActionResult newtime()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<showtime> sh = db.showtimes.ToList();
           
            return View(sh);
        }
        public ActionResult selectseats()
        {
            return View();
        }

        public ActionResult addfooditems()
        {
            return View();
        }

        public ActionResult registershops()
        {
            return View();
        }
    }
}