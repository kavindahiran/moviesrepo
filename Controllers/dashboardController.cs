﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using RatingApp.Helper;
using RatingApp.Models;
using ZXing;

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
            if (Session["id"] != null)
            {

                if ((bool)Session["roleid"] == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("custom404", "errors");
                }
            }
            else
            {
                return RedirectToAction("usernotfound", "errors");
            }

        }
        [HttpPost]
        public ActionResult addmovie(Movie_Item imageModel, string awardnominated, string banned)
        {

            byte[] imagebyte = null;
            // var file = imageModel.ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.imgpath = "~/movieIcons/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/movieIcons/"), fileName);
            BinaryReader reader = new BinaryReader(imageModel.ImageFile.InputStream);
            imagebyte = reader.ReadBytes(imageModel.ImageFile.ContentLength);
            imageModel.ImageFile.SaveAs(fileName);

            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                if (awardnominated == "1")
                {
                    imageModel.awardnominated = true;
                }
                else
                {
                    imageModel.awardnominated = false;
                }
                if (banned == "1")
                {
                    imageModel.banned = true;
                }
                else
                {
                    imageModel.banned = false;
                }
                imageModel.imageCover = imagebyte;
                db.Movie_Item.Add(imageModel);
                db.SaveChanges();
                string mess = "Content Created successfully";
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
        [HttpGet]
        public ActionResult addnewsforum()
        {
            if (Session["id"] != null)
            {

                if ((bool)Session["roleid"] == true)
                {

                    return View();

                }
                else
                {
                    return RedirectToAction("custom404", "errors");
                }
            }
            else
            {
                return RedirectToAction("usernotfound", "errors");
            }
         
        }
        [HttpPost]
        public ActionResult addnewsforum(newsforum imageModel, DateTime ndate)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.imagepath = "~/newsfolder/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/newsfolder/"), fileName);
            //BinaryReader reader = new BinaryReader(imageModel.ImageFile.InputStream);
            //imagebyte = reader.ReadBytes(imageModel.ImageFile.ContentLength);
            imageModel.ImageFile.SaveAs(fileName);

            using (moviedetailsdb1 db = new moviedetailsdb1())
            {

                imageModel.date = ndate;
                db.newsforums.Add(imageModel);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            return View();
        }
        [HttpGet]
        public ActionResult addnewscontainer()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<newsforum> list = db.newsforums.ToList();


            ViewBag.list1 = new SelectList(list, "id", "title");

            if (Session["id"] != null)
            {

                if ((bool)Session["roleid"] == true)
                {

                    return View();

                }
                else
                {
                    return RedirectToAction("custom404", "errors");
                }
            }
            else
            {
                return RedirectToAction("usernotfound", "errors");
            }

          
        }
        [HttpPost]
        public ActionResult addnewscontainer(newscontainer imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.imagepath = "~/newsfolder/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/newsfolder/"), fileName);
            //BinaryReader reader = new BinaryReader(imageModel.ImageFile.InputStream);
            //imagebyte = reader.ReadBytes(imageModel.ImageFile.ContentLength);
            imageModel.ImageFile.SaveAs(fileName);

            using (moviedetailsdb1 db = new moviedetailsdb1())
            {


                db.newscontainers.Add(imageModel);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            moviedetailsdb1 db1 = new moviedetailsdb1();
            List<newsforum> list = db1.newsforums.ToList();


            ViewBag.list1 = new SelectList(list, "id", "title");
            return View();
        }

        public ActionResult newsdetails(int? id)
        {
            indexnewsforum model = new indexnewsforum();
            moviedetailsdb1 db = new moviedetailsdb1();

            model.nf = db.newsforums.Find(id);
            model.con = db.newscontainers.Where(x => x.newTid == id).ToList();
            return View(model);
        }

        public ActionResult addArena()
        {
            if (Session["id"] != null)
            {

                if ((bool)Session["roleid"] == true)
                {

                    return View();

                }
                else
                {
                    return RedirectToAction("custom404", "errors");
                }
            }
            else
            {
                return RedirectToAction("usernotfound", "errors");
            }
          
        }
        [HttpPost]
        public ActionResult addArena(heroarenatable ha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ha.ImageFile.FileName);
                    string extension = Path.GetExtension(ha.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ha.imagepath = "~/movieIcons/heroarena/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/movieIcons/heroarena/"), fileName);
                    ha.ImageFile.SaveAs(fileName);
                    using (moviedetailsdb1 db = new moviedetailsdb1())
                    {
                        db.heroarenatables.Add(ha);
                        db.SaveChanges();
                        string mess = "submitted successfully";
                        ViewBag.message = mess;
                    }
                }
                ModelState.Clear();
                return View();
            }
            catch(Exception ex)
            {
                throw new Exception("Something went wrong");
            }

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

        public ActionResult newview(int id)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var movieI = db.Movie_Item.Find(id);
            PhotoGallery pg = new PhotoGallery();


            List<PhotoGallery> ph = db.PhotoGalleries.Where(x => x.moviPhotoId == id).Take(4).ToList();
            var moviegallery = db.PhotoGalleries.FirstOrDefault(x => x.moviPhotoId == id);
            int userid = Convert.ToInt32(Session["id"]);
            if (userid == 0)
            {
                var moviItem = db.Review_Table.Where(x => x.movieT_ID == id).ToList();
                ViewBag.test = moviItem;
            }
            else
            {
                var moviItem = db.Review_Table.Where(x => x.userID == userid & x.movieT_ID == id).ToList();
                ViewBag.test = moviItem;
            }

            var rt1 = db.Review_Table.Where(x => x.movieT_ID == id).Select(x => x.rating).Average();

          
            var userliked = db.movielikes.Where(x => x.movieid == id && x.userid == userid && x.likecount == 1).Count();
            ViewBag.liked = userliked;
            var likecount = db.movielikes.Where(x => x.movieid == id).Count();
            ViewBag.likesc = likecount;


            ViewBag.getratings = rt1;
            ViewBag.gall = ph;
            ViewBag.product = movieI;
            var review = new Review_Table()
            {
                movieT_ID = movieI.movie_id
            };
            return View("newview", review);

        }
        [HttpPost]
        public ActionResult movielikes(Review_Table rt)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            movielike lk = new movielike();
            var likecount = db.movielikes.Where(x => x.movieid == rt.movieT_ID).Count();
            ViewBag.likesc = likecount;
            int userid = Convert.ToInt32(Session["id"]);
            lk.movieid = rt.movieT_ID;
            lk.userid = userid;
            if (lk.likecount == null)
            {
                lk.likecount = 1;
            }
            else
            {
                lk.likecount = lk.likecount + 1;

            }
            var searchdata = db.movielikes.Where(x => x.userid == userid && x.movieid == rt.movieT_ID).AsNoTracking().FirstOrDefault();
            if (searchdata == null)
            {
                db.movielikes.Add(lk);
                db.SaveChanges();
                return RedirectToAction("newview", "dashboard", new { id = rt.movieT_ID });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    lk.movielike1 = searchdata.movielike1;
                    db.Entry(lk).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("newview", "dashboard", new { id = rt.movieT_ID });
                }
                else
                {
                    Response.StatusCode = 400;
                    return Content("Naughty");
                }
            }

          
        }

        public ActionResult details(int id)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var movieI = db.Movie_Item.Find(id);
            PhotoGallery pg = new PhotoGallery();


            List<PhotoGallery> ph = db.PhotoGalleries.Where(x => x.moviPhotoId == id).Take(4).ToList();
            var moviegallery = db.PhotoGalleries.FirstOrDefault(x => x.moviPhotoId == id);
            int userid = Convert.ToInt32(Session["id"]);
            if (userid == 0)
            {
                var moviItem = db.Review_Table.Where(x => x.movieT_ID == id).ToList();
                ViewBag.test = moviItem;
            }
            else
            {
                var moviItem = db.Review_Table.Where(x => x.userID == userid & x.movieT_ID == id).ToList();
                ViewBag.test = moviItem;
            }



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
            // rev.DatePost = DateTime.Now;
            rev.userID = db.usertbls.Single(x => x.username.Equals(username)).user_id;
            rev.DatePost = DateTime.Now;

            var searchdata = db.Review_Table.Where(x => x.userID == rev.userID && x.movieT_ID == rev.movieT_ID).AsNoTracking().FirstOrDefault();
            if (searchdata == null)
            {
                db.Review_Table.Add(rev);
                db.SaveChanges();
                return RedirectToAction("newview", "dashboard", new { id = rev.movieT_ID });
            }
            else
            {

                if (ModelState.IsValid)
                {
                    rev.review_id = searchdata.review_id;
                    db.Entry(rev).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("newview", "dashboard", new { id = rev.movieT_ID });
                }
                else
                {
                    return RedirectToAction("addmovie", "dashboard");
                }
            }

        }

        [HttpPost]
        public ActionResult sendcastReview(castratingsT rev, double rating)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            // var searchdata = db.Review_Table.Where(x => x.userID == rev.userID && x.movieT_ID == rev.movieT_ID).SingleOrDefault();
            // if (searchdata == null)

            rev.castratings = rating;
            string username = Session["regname"].ToString();
            // rev.DatePost = DateTime.Now;
            rev.userid = db.usertbls.Single(x => x.username.Equals(username)).user_id;


            var searchdata = db.castratingsTs.Where(x => x.userid == rev.userid && x.castid == rev.castid).AsNoTracking().FirstOrDefault();
            if (searchdata == null)
            {
                db.castratingsTs.Add(rev);
                db.SaveChanges();
                return RedirectToAction("castdetails", "dashboard", new { id = rev.castid });
            }
            else
            {

                if (ModelState.IsValid)
                {
                    rev.castRid = searchdata.castRid;
                    db.Entry(rev).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("castdetails", "dashboard", new { id = rev.castid });
                }
                else
                {
                    return RedirectToAction("addmovie", "dashboard");
                }
            }




        }


        public ActionResult castdetails(int id, int? page)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var movieI = db.movieCastcrews.Find(id);
            castphotogallery pg = new castphotogallery();

            List<castphotogallery> ph = db.castphotogalleries.Where(x => x.castTableid == id).Take(4).ToList();
            var moviegallery = db.castphotogalleries.FirstOrDefault(x => x.castTableid == id);
            List<movieCastcrew> mvc = new List<movieCastcrew>();


            ViewBag.gall = ph;
            ViewBag.product = movieI;

            var cstatus = db.movieCastcrews.Where(x => x.cast_id == id).Select(x => x.cast_name).First();

            ViewBag.gallerycastname = cstatus;

            int userid = Convert.ToInt32(Session["id"]);
            if (userid == 0)
            {
                var moviItem = db.castratingsTs.Where(x => x.castid == id).ToList();
                ViewBag.test = moviItem;
            }
            else
            {
                var moviItem = db.castratingsTs.Where(x => x.userid == userid & x.castid == id).ToList();
                ViewBag.test = moviItem;
            }
            List<castComment> cl = new List<castComment>();
            var com1 = from c in cl
                       group c by c.ccid into group1
                       select new
                       {
                           count = group1.Count()
                       };

            ViewBag.countC = db.castComments.Count();
            var userliked = db.castlikes.Where(x => x.castid == id && x.userid == userid && x.likecount == 1).Count();
            ViewBag.liked = userliked;
           var likecount = db.castlikes.Where(x=>x.castid==id).Count();
            ViewBag.likesc = likecount;


            List<castComment> com = db.castComments.Include(y => y.castreplies).Where(x => x.castrid == id).OrderByDescending(x => x.comdate).ToList();
            PagedList<castComment> model = new PagedList<castComment>(com, page ?? 1, 4);
            ViewBag.comments = model;

            var review = new castratingsT()
            {
                castid = movieI.cast_id
            };
            return View("castdetails", review);



        }

        public ActionResult castlikes(castratingsT rt)
        {
           
            moviedetailsdb1 db = new moviedetailsdb1();
            castlike lk = new castlike();
            var likecount = db.castlikes.Where(x => x.castid == rt.castid).Count();
            ViewBag.likesc = likecount;
            int userid =Convert.ToInt32(Session["id"]);
            lk.castid = rt.castid;
            lk.userid = userid;
            if (lk.likecount == null)
            {
                lk.likecount = 1;
            }
            else
            {
                lk.likecount = lk.likecount + 1;

            }
            var searchdata = db.castlikes.Where(x => x.userid == userid && x.castid == rt.castid).AsNoTracking().FirstOrDefault();
            if (searchdata == null)
            {
                db.castlikes.Add(lk);
                db.SaveChanges();
                return RedirectToAction("castdetails", "dashboard", new { id = rt.castid });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    lk.likeid = searchdata.likeid;
                    db.Entry(lk).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("castdetails", "dashboard", new { id = rt.castid });
                }
                else
                {
                    Response.StatusCode = 400;
                    return Content("Naughty");
                }
            }

               
        }


        public ActionResult GetPaggedData(int pageNumber = 1, int pageSize = 20)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<castComment> listData = db.castComments.ToList();
            var pagedData = Pagination.PagedResult(listData, pageNumber, pageSize);
            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult castreplies(int cid, string replys, int userid, int castid)
        {

            if (Session["id"] != null)
            {
                moviedetailsdb1 db = new moviedetailsdb1();
                castreply rip = new castreply();
                rip.Castcomid = cid;
                rip.userid = Convert.ToInt32(Session["id"].ToString());
                rip.replydate = DateTime.Now;
                rip.replys = replys;
                db.castreplies.Add(rip);

                db.SaveChanges();
            }
            int casti = Convert.ToInt32(castid);
            //        return RedirectToAction("Adtocart", new RouteValueDictionary(
            //new { controller ="Shop", action = "Adtocart", Id = movieid }));
            return RedirectToAction("castdetails", "dashboard", new { id = castid });
            //return RedirectToAction("castdetails", new movieCastcrew { id = casti });

        }

        public ActionResult postcom(string cimtxt, castratingsT ct)
        {
            if (Session["id"] != null)
            {
                moviedetailsdb1 db = new moviedetailsdb1();
                castComment cm = new castComment();
                cm.comments = cimtxt;
                cm.castrid = ct.castid;
                cm.comdate = DateTime.Now;
                cm.userid = Convert.ToInt32(Session["id"]);
                db.castComments.Add(cm);
                db.SaveChanges();

            }
            //return  RedirectToAction("castdetails", new movieCastcrew { cast_id =Convert.ToInt32(ct.castid) });
            return RedirectToAction("castdetails", "dashboard", new { id = ct.castid });

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
        public ActionResult addActor(movieCastcrew cast, string castawardsnominated, string died)
        {

            // var file = imageModel.ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(cast.ImageFile.FileName);
            string extension = Path.GetExtension(cast.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            cast.cast_profile = "~/castImage/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/castImage/"), fileName);
            cast.ImageFile.SaveAs(fileName);

            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                if (castawardsnominated == "1")
                {
                    cast.castawardsnominated = true;
                }
                else
                {
                    cast.castawardsnominated = false;
                }
                if (died == "1")
                {
                    cast.died = true;
                }
                else
                {
                    cast.died = false;
                }

                db.movieCastcrews.Add(cast);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            moviedetailsdb1 db1 = new moviedetailsdb1();
            List<Movie_Item> list = db1.Movie_Item.ToList();


            ViewBag.list1 = new SelectList(list, "movie_id", "Movie_name");
            //  imageid = imageModel.id;

            return View();
        }


        public ActionResult addfood()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<halltable> list = db.halltables.ToList();


            ViewBag.list1 = new SelectList(list, "hallid", "hallname");



            return View();
        }
        [HttpPost]
        public ActionResult addfood(HallFoodTbl cast)
        {
            string fileName = Path.GetFileNameWithoutExtension(cast.ImageFile.FileName);
            string extension = Path.GetExtension(cast.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            cast.foodimage = "~/hallfoodlist/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/hallfoodlist/"), fileName);
            cast.ImageFile.SaveAs(fileName);

            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {


                db.HallFoodTbls.Add(cast);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            moviedetailsdb1 db1 = new moviedetailsdb1();
            List<halltable> list = db1.halltables.ToList();


            ViewBag.list1 = new SelectList(list, "hallid", "hallname");
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
        public ActionResult addShopitem(shoppingcart imageModel, Movie_Item md)
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
            if (userid == 0)
            {
                return RedirectToAction("usernotfound", "errors");
            }
            moviedetailsdb1 db = new moviedetailsdb1();

            var show = db.usertbls.Find(userid);
            return View(show);
        }

        public ActionResult identifyA()
        {
            return View();
        }

        public ActionResult addnewmovieshow()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            showingmodelcollection col = new showingmodelcollection();

            return View();
        }
        [HttpPost]
        public ActionResult addnewshow(nowshowing coll, DateTime expire)
        {
            byte[] imagebyte = null;
            string fileName = Path.GetFileNameWithoutExtension(coll.ImageFile.FileName);
            string extension = Path.GetExtension(coll.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            coll.imagepath = "~/nowShowing/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/nowShowing/"), fileName);
            BinaryReader reader = new BinaryReader(coll.ImageFile.InputStream);
            imagebyte = reader.ReadBytes(coll.ImageFile.ContentLength);
            coll.ImageFile.SaveAs(fileName);
            coll.availableDate = DateTime.Now;
            coll.expireDate = expire;
            coll.image = imagebyte;
            //file.SaveAs(Server.MapPath("~/movieIcons/" + file.FileName));
            using (moviedetailsdb1 db = new moviedetailsdb1())
            {
                db.nowshowings.Add(coll);
                db.SaveChanges();
                string mess = "submitted successfully";
                ViewBag.message = mess;
            }
            ModelState.Clear();
            return RedirectToAction("addnewmovieshow");
        }

        bool val;

        public ActionResult chatpopup()
        {
            moviedetailsdb1 db = new moviedetailsdb1();

            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                var chats = db.chatmessages.Where(y => y.userid == x).ToList();
                ViewBag.image = Session["profpic"].ToString();
                ViewBag.responce = val;
                var text = "Your message has been recoreded we will contact you as soon as possible";
                ViewBag.mess = text;

                return View(chats);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }




        }
        [HttpPost]
        public ActionResult replyadmin(chatmessage ms, string message)
        {

            moviedetailsdb1 db = new moviedetailsdb1();
            if (Session["id"] != null)
            {
                int x = Convert.ToInt32(Session["id"]);
                string username = Session["regname"].ToString();
                chatmessage chat = new chatmessage();
                chat.chatmessage1 = message;
                chat.userid = x;
                chat.username = username;
                chat.chattime = DateTime.Now;
                db.chatmessages.Add(chat);
                db.SaveChanges();
                val = true;
                TempData["signal"] = "okey";
                ViewBag.responce = val;
                var text = "Your message has been recorede we will contact you as soon as possible";
                ViewBag.mess = text;
                return RedirectToAction("chatpopup", "dashboard");
            }
            else
            {
                return RedirectToAction("chatpopup", "dashboard");
            }
        }




        public ActionResult print()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<Movie_Item> x = db.Movie_Item.ToList();
            return View(x);
        }

        [HttpPost]
        public ActionResult Generate(string prodectId, Movie_Item mi, string movieid)
        {
            var barcodeWriterPixelData = new BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 200,
                    Width = 200,
                    Margin = 0
                }
            };
            var pixelData = barcodeWriterPixelData.Write(prodectId);
            using (Bitmap bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(
                            pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    ViewBag.BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            var barcodeWriterPixelData1 = new BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 200,
                    Width = 200,
                    Margin = 0
                }
            };
            var pixelData1 = barcodeWriterPixelData1.Write(prodectId);
            using (Bitmap bitmap = new Bitmap(pixelData1.Width, pixelData1.Height, PixelFormat.Format32bppRgb))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData1.Width, pixelData1.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(
                            pixelData1.Pixels, 0, bitmapData.Scan0, pixelData1.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    ViewBag.barcode = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }


            ViewBag.prodectId = prodectId;


            moviedetailsdb1 db = new moviedetailsdb1();

            barcodeT bt = new barcodeT();
            bt.barcode = prodectId;
            bt.movieid = Convert.ToInt32(movieid);
            db.barcodeTs.Add(bt);
            db.SaveChanges();
            List<Movie_Item> x = db.Movie_Item.ToList();
            return View("print", x);
        }



    }
}
