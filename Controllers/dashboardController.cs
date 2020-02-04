﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RatingApp.Models;
namespace RatingApp.Controllers
{
    public class dashboardController : Controller
    {
        int imageid = 0;

        // GET: dashboard
        public ActionResult mydash()
        {
            return View();
        }
        [HttpGet]
        public ActionResult addmovie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addmovie(Movie_Item imageModel)
        {

            // var file = imageModel.ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.imgpath = "~/movieIcons/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/movieIcons/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);

            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                db.Movie_Item.Add(imageModel);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            //  imageid = imageModel.id;

            return View();
        }


        [HttpPost]
        public ActionResult ImageUpload(movietbl1 model)
        {

            var file = model.ImageFile;


            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);




                file.SaveAs(Server.MapPath("/movieIcons/" + file.FileName));








            }

            return Json(file.FileName, JsonRequestBehavior.AllowGet);

        }



        public ActionResult addArena()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addArena(heroarenatable ha)
        {


            string fileName = Path.GetFileNameWithoutExtension(ha.ImageFile.FileName);
            string extension = Path.GetExtension(ha.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            ha.imagepath = "~/movieIcons/heroarena" + fileName;
            fileName = Path.Combine(Server.MapPath("~/movieIcons/heroarena"), fileName);
            ha.ImageFile.SaveAs(fileName);
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                db.heroarenatables.Add(ha);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            return View();
        }


        public ActionResult addtrailer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addtrailer(trailertb tb)
        {


            string fileName = Path.GetFileNameWithoutExtension(tb.ImageFile.FileName);
            if (tb.ImageFile.ContentLength < 1064857600)
            {
                string extension = Path.GetExtension(tb.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                tb.path = "~/videofiles/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/videofiles/"), fileName);
                tb.ImageFile.SaveAs(fileName);
                /*   using (moviesdb db = new moviesdb())
                   {
                       db.trailertbs.Add(tb);
                       db.SaveChanges();
                       string mess = "submitted successfully";
                       ViewBag.message = mess;
                   }*/
            }
            else
            {
                string mess = "file is too large";
                ViewBag.message = mess;
            }


            /*  
                     string fileName1 = Path.GetFileNameWithoutExtension(tb.ImageFile1.FileName);
              string extension1 = Path.GetExtension(tb.ImageFile1.FileName);
              fileName1 = fileName1 + DateTime.Now.ToString("yymmssfff") + extension1;
              tb.coverimage = "~/videofiles/" + fileName1;
              fileName1 = Path.Combine(Server.MapPath("~/videofiles/"), fileName1);
              tb.ImageFile1.SaveAs(fileName1);
              using (moviesdb db = new moviesdb())
              {
                  db.trailertbs.Add(tb);
                  db.SaveChanges();
                  string mess = "submitted successfully";
                  ViewBag.message = mess;
              }

              ModelState.Clear();*/
            return View();

        }

        public ActionResult details(int id)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var movieI = db.Movie_Item.Find(id);
            PhotoGallery pg = new PhotoGallery();


            List<PhotoGallery> ph = db.PhotoGalleries.Where(x => x.moviPhotoId == id).ToList();
            var moviegallery = db.PhotoGalleries.FirstOrDefault(x => x.moviPhotoId == id);

            ViewBag.gall = ph;
            ViewBag.product = movieI;
            var review = new Review_Table()
            {
                movieT_ID = movieI.movie_id
            };
            return View("details", review);


        }
        [HttpPost]
        public ActionResult sendReview(Review_Table rev, double rating)
        {
            /*moviedetailsdb1 db1 = new moviedetailsdb1();
            string username1 = Session["regname"].ToString();
            rev.userID = db1.usertbls.Single(x => x.username.Equals(username1)).user_id;*/
            moviedetailsdb1 db = new moviedetailsdb1();
            // var searchdata = db.Review_Table.Where(x => x.userID == rev.userID && x.movieT_ID == rev.movieT_ID).SingleOrDefault();
            // if (searchdata == null)

            rev.rating = rating;
            string username = Session["regname"].ToString();
            rev.DatePost = DateTime.Now;
            rev.userID = db.usertbls.Single(x => x.username.Equals(username)).user_id;


            var searchdata = db.Review_Table.Where(x => x.userID == rev.userID && x.movieT_ID == rev.movieT_ID).AsNoTracking().FirstOrDefault();
            if (searchdata == null)
            {
                db.Review_Table.Add(rev);
                db.SaveChanges();
                return RedirectToAction("details", "dashboard", new { id = rev.movieT_ID });
            }
            else
            {

                if (ModelState.IsValid)
                {
                    rev.review_id = searchdata.review_id;
                    db.Entry(rev).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("details", "dashboard", new { id = rev.movieT_ID });
                }
                else
                {
                    return RedirectToAction("addmovie", "dashboard");
                }
            }

        }

        public ActionResult imageGallery()
        {

            moviedetailsdb1 db = new moviedetailsdb1();







            List<Movie_Item> list = db.Movie_Item.ToList();
            ViewBag.DepartmentList = new SelectList(list, "movie_id", "movie_id");
            return View();



        }

        public ActionResult imau(customimagemodel model)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<Movie_Item> list = db.Movie_Item.ToList();
            ViewBag.DepartmentList = new SelectList(list, "movie_id", "movie_id");

            int imageId = 0;

            var file = model.ImageFile;

            byte[] imagebyte = null;

            if (file != null)
            {

                file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName));

                BinaryReader reader = new BinaryReader(file.InputStream);

                imagebyte = reader.ReadBytes(file.ContentLength);

                //ImageStore img = new ImageStore();
                // model.moviPhotoId = model.Movie_Item.movie_id;
                // model.moviPhotoId = md.movie_id;
                PhotoGallery pg = new PhotoGallery();
                pg.moviPhotoId = model.movie_id;
                pg.movietitle = file.FileName;
                pg.binary = imagebyte;
                pg.ipath = "/UploadedImage/" + file.FileName;

                db.PhotoGalleries.Add(pg);
                db.SaveChanges();

                imageId = model.photoID;

            }

            return Json(file.FileName, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ImageRetrieve(int photoID)
        {
            moviedetailsdb1 db = new moviedetailsdb1();

            var img = db.PhotoGalleries.SingleOrDefault(x => x.photoID == photoID);

            return File(img.binary, "image/jpg");


        }


        public ActionResult mapLocation()
        {
            return View();
        }


        public ActionResult GetNearByLocations(string Currentlat, string Currentlng)
        {
            using (var context = new mapgoodb())
            {
                var currentLocation = DbGeography.FromText("POINT( " + Currentlng + " " + Currentlat + " )");

                //var currentLocation = DbGeography.FromText("POINT( 78.3845534 17.4343666 )");

                var places = (from u in context.Landmarks1
                              orderby u.GeoLocation.Distance(currentLocation)
                              select u).Take(4).Select(x => new RatingApp.Models.Landmark() { Name = x.LandmarkName, lat = x.GeoLocation.Latitude, lng = x.GeoLocation.Longitude, Distance = x.GeoLocation.Distance(currentLocation) });
                var nearschools = places.ToList();

                return Json(nearschools, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult addActor()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<Movie_Item> list = db.Movie_Item.ToList();


            ViewBag.list1 = new SelectList(list, "movie_id", "Movie_name");
            return View();
        }

        [HttpPost]
        public ActionResult addActor(movieCastcrew cast)
        {

            // var file = imageModel.ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(cast.ImageFile.FileName);
            string extension = Path.GetExtension(cast.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            cast.cast_profile = "~/castImage/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/movieIcons/"), fileName);
            cast.ImageFile.SaveAs(fileName);

            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                db.movieCastcrews.Add(cast);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            //  imageid = imageModel.id;

            return View();
        }


        public ActionResult castphotogalley()
        {

            moviedetailsdb1 db = new moviedetailsdb1();







            List<movieCastcrew> list = db.movieCastcrews.ToList();
            ViewBag.DepartmentList = new SelectList(list, "cast_id", "cast_id");
            return View();



        }


        [HttpPost]
        public ActionResult castgalleysendimage(customizableCastAndMovie model)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<Movie_Item> list = db.Movie_Item.ToList();
            ViewBag.DepartmentList = new SelectList(list, "cast_id", "cast_id");

            int imageId = 0;

            var file = model.ImageFile;

            byte[] imagebyte = null;

            if (file != null)
            {

                file.SaveAs(Server.MapPath("/castImageFolder/" + file.FileName));

                BinaryReader reader = new BinaryReader(file.InputStream);

                imagebyte = reader.ReadBytes(file.ContentLength);

                //ImageStore img = new ImageStore();
                // model.moviPhotoId = model.Movie_Item.movie_id;
                // model.moviPhotoId = md.movie_id;
                castphotogallery pg = new castphotogallery();
                pg.castTableid = model.cast_id;
                pg.castimagename = file.FileName;
                pg.imageC = imagebyte;
                pg.castIpath = "/castImageFolder/" + file.FileName;

                db.castphotogalleries.Add(pg);
                db.SaveChanges();

                imageId = model.id;

            }

            return Json(file.FileName, JsonRequestBehavior.AllowGet);
        }



        public ActionResult castgalleyretrieve(int photoID)
        {
            moviedetailsdb1 db = new moviedetailsdb1();

            var img = db.castphotogalleries.SingleOrDefault(x => x.id == photoID);

            return File(img.imageC, "image/jpg");


        }
        public ActionResult addShopitem()
        {
            moviedetailsdb1 db1 = new moviedetailsdb1();
            List<Movie_Item> mi = db1.Movie_Item.ToList();
            ViewBag.itemlist = new SelectList(mi, "movie_id", "movie_id");
            return View();
        }
        [HttpPost]
        public ActionResult addShopitem(shoppingcart imageModel,Movie_Item md)
        {
            moviedetailsdb1 db1 = new moviedetailsdb1();
            List<Movie_Item> mi = db1.Movie_Item.ToList();
            ViewBag.itemlist = new SelectList(mi, "movie_id", "movie_id");

            
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);

            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.coverphotopath = "~/coverphotoes/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/coverphotoes/"), fileName);
            imageModel.isavailable = true;

            int x = md.movie_id;
            imageModel.movieItemDis = x;
            imageModel.ImageFile.SaveAs(fileName);

            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                db.shoppingcarts.Add(imageModel);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            return View();
        }


        public ActionResult viewprofile()
        {
            int userid = Convert.ToInt32(Session["id"]);
            if(userid==0)
            {
                return RedirectToAction("Index", "Home");
            }
            moviedetailsdb1 db = new moviedetailsdb1();
          
            var show = db.usertbls.Find(userid);
            return View(show); 
        }




    }
}