using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RatingApp.Models;
using System.Data.Entity;
using System.Web.Routing;
using System.Data.Entity.Spatial;
using RatingApp.Models.billviewmodel;
using RatingApp.Models.ViewModels;

namespace RatingApp.Controllers
{
    public class ShopController : Controller
    {
        float x = 0;
        // GET: Shop
        public ActionResult Index()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            shopviewmodel shop = new shopviewmodel();

            if (TempData["tempcart"] != null)
            {
               
                List<tempcart> tc = TempData["tempcart"] as List<tempcart>;
                foreach (var item in tc)
                {
                    x += item.bill;
                }
                TempData["total"] = x;

            }
            TempData.Keep();

            List<Movie_Item> moviI = db.Movie_Item.ToList();
            List<PhotoGallery> pg = db.PhotoGalleries.ToList();
            List<shoppingcart> cart = db.shoppingcarts.ToList();
            string  text1= "Available";
            ViewBag.title1 = text1;
            var multiple = from movie in moviI
                           orderby movie.movie_id
                           join carty in cart on movie.movie_id equals carty.movieItemDis
                           where (carty.isavailable == true)
                           select new shopviewmodel { movieitems = movie, cart = carty };



            return View(multiple);
        }
        [HttpGet]
        public ActionResult Adtocart(int? movie_id)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            Movie_Item it = db.Movie_Item.Where(x => x.movie_id == movie_id).SingleOrDefault();
            shopviewmodel shop = new shopviewmodel();
           int userid= Convert.ToInt32(Session["id"].ToString());
            shop.movieitems = it;
            shoppingcart sc = db.shoppingcarts.Where(x => x.movieItemDis == movie_id).SingleOrDefault();
            Review_Table rv1 = db.Review_Table.Where(x => x.movieT_ID == movie_id).FirstOrDefault();
            
            shop.rv = rv1;

            List <reply> rep = db.replies.ToList();
            List<comment> comi = db.comments.ToList();
            shop.cart = sc;

            List<comment> com = db.comments.Include(y=>y.replies).Where(x=>x.movieid==movie_id).OrderByDescending(x=>x.commentdate).ToList();
            shop.cm = com;


            ViewBag.repl=from repi in rep
                         join co in comi on repi.commentid equals co.cid
                         select new { rep = repi, cm = co};
            
           

            ViewBag.MovieId = movie_id;

            return View(shop);
        }
        List<tempcart> carty = new List<tempcart>();
        //[HttpPost]
        //public ActionResult Adtocart(shopviewmodel sm, string qty, int movie_id)
        //{
        //    tempcart tc = new tempcart();
        //    tc.productid = sm.movieitems.movie_id;
        //    tc.productname = sm.movieitems.Movie_name;
        //    tc.price = (float)sm.cart.price;
        //    tc.qty = Convert.ToInt32(qty);
        //    tc.bill = (float)sm.cart.price * tc.qty;
            
        //    if (TempData["tempcart"] == null)
        //    {
        //        carty.Add(tc);
        //        TempData["tempcart"] = carty;
        //    }
        //    else
        //    {
        //        List<tempcart> carty2 = TempData["tempcart"] as List<tempcart>;
        //        carty2.Add(tc);
        //        TempData["tempcart"] = carty2;
        //    }
        //    TempData.Keep();

        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult checkout()
        {
          TempData.Keep();
           

            return View();
        }
        [HttpPost]
        public ActionResult checkout(ordertable o)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<tempcart> li = TempData["tempcart"] as List<tempcart>;
            inovice inv = new inovice();
            inv.userTblid = Convert.ToInt32(Session["id"].ToString());
            inv.orderdate = System.DateTime.Now;
            //inv.orderid = o.orderid;
            inv.totalbill = (int)x;
            
            db.inovices.Add(inv);
            db.SaveChanges();

            foreach(var item in li)
            {
                ordertable ord = new ordertable();
                ord.prodid = item.productid;
                ord.invoiceid = inv.invoiceid;
                ord.orderDate = DateTime.Now;
                ord.quantity = item.qty;
                ord.unitprice = (int)item.price;
                ord.bill = (int)item.bill;
                db.ordertables.Add(ord);
                db.SaveChanges();

            }
            TempData.Remove("total");
            TempData.Remove("tempcart");
            TempData["msg"] = "Transaction Completed...";
            TempData.Keep();

            return RedirectToAction("Index");

        }

        public ActionResult getdata(shopviewmodel sm, string qty)
        {
            tempcart tc = new tempcart();

            tc.productid = sm.movieitems.movie_id;
            tc.Ipath = sm.movieitems.imgpath;
            tc.productname = sm.movieitems.Movie_name;
            tc.price = (float)sm.cart.price;
            tc.qty = Convert.ToInt32(qty);
            tc.bill = (float)sm.cart.price * tc.qty;

            if (TempData["tempcart"] == null)
            {
                carty.Add(tc);
                TempData["tempcart"] = carty;
            }
            else
            {
                List<tempcart> carty2 = TempData["tempcart"] as List<tempcart>;
                int flag = 0;
                foreach(var item in carty2)
                {
                    if (item.productid == tc.productid)
                    {
                        item.qty += tc.qty;
                        item.bill += tc.bill;
                        flag = 1;
                    }
                }
                if(flag==0)
                {
                    carty2.Add(tc);
                }
                
                TempData["tempcart"] = carty2;
            }
            TempData.Keep();

            return RedirectToAction("Index");
        }

        public ActionResult like(shopviewmodel sm)
        {

            moviedetailsdb1 db = new moviedetailsdb1();
            int userid = Convert.ToInt32(Session["id"].ToString());
            var data = db.Review_Table.AsNoTracking().ToList().Find(x => x.movieT_ID == sm.movieitems.movie_id && x.userID == userid);
            Review_Table riv = new Review_Table();
            riv.movielikes = 0;
            riv.movieT_ID = sm.movieitems.movie_id;
            riv.userID = db.usertbls.Single(x => x.user_id.Equals(userid)).user_id;
            riv.DatePost = DateTime.Now;
            riv.movielikes = sm.rv.movielikes + 1;
            if (data == null)
            {
                db.Review_Table.Add(riv);
                db.SaveChanges();


            }
            else
            {
                if (ModelState.IsValid)
                {



                    
                    riv.review_id = data.review_id;
                    db.Entry(riv).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }


/*
            moviedetailsdb1 db = new moviedetailsdb1();
            int userid = Convert.ToInt32(Session["id"].ToString());
            Review_Table rt = db.Review_Table.ToList().Find(x => x.movieT_ID == sm.movieitems.movie_id && x.userID== userid);
            rt.movielikes=rt.movielikes+1;
            db.Review_Table.Add(rt);
            db.SaveChanges();*/
            return RedirectToAction("Adtocart", new { movie_id = sm.movieitems.movie_id });
        }

        public ActionResult addcom()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var comm = db.comments.Include(x => x.replies).ToList();
            return View(comm);
        }

        public ActionResult postreply(string reply, int cid, int movieid, int userid)
        {
          
               
       
             if (Session["id"]!=null)
            {
                moviedetailsdb1 db = new moviedetailsdb1();
                reply rip = new reply();
                rip.commentid = cid;
                rip.useridR = Convert.ToInt32(Session["id"].ToString()); 
                rip.replytime = DateTime.Now;
                rip.replystatus = reply;
                db.replies.Add(rip);
                db.SaveChanges();
            }
            //        return RedirectToAction("Adtocart", new RouteValueDictionary(
            //new { controller ="Shop", action = "Adtocart", Id = movieid }));
            return RedirectToAction("Adtocart", new { movie_id = movieid });
            
        }
        [HttpPost]
        public ActionResult postcomment(string cmtbox, int movieid)
        {
            if (Session["id"] != null)
            {
                moviedetailsdb1 db = new moviedetailsdb1();
                comment com = new comment();
                com.commentline = cmtbox;
                com.movieid = movieid;
                com.commentdate = DateTime.Now;
                com.userid = Convert.ToInt32(Session["id"].ToString());
                db.comments.Add(com);
                db.SaveChanges();
            }
            return RedirectToAction("Adtocart", new { movie_id = movieid});

        }


        public ActionResult billing()
        {
            TempData.Keep();
            ViewBag.userN = Session["regname"];
            return View();
        }
        [HttpPost]
        public ActionResult billing(string name, string address, string address1, string zipcode, string city, string country, string cnum, System.DateTime expdate, string code)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<tempcart> li = TempData["tempcart"] as List<tempcart>;
            inovice inv = new inovice();
            inv.userTblid = Convert.ToInt32(Session["id"].ToString());
            inv.orderdate = System.DateTime.Now;
            //inv.orderid = o.orderid;
            inv.totalbill = (int)x;
            inv.fullname = name;
            inv.address1 = address;
            inv.zipcode = Convert.ToInt32(zipcode);
            inv.city = city;
            inv.country = country;
            
            db.inovices.Add(inv);
            db.SaveChanges();
            TempData["invoiceno"] = inv.invoiceid;
            foreach (var item in li)
            {
                ordertable ord = new ordertable();
                ord.prodid = item.productid;
                ord.invoiceid = inv.invoiceid;
                ord.orderDate = DateTime.Now;
                ord.quantity = item.qty;
                ord.unitprice = (int)item.price;
                ord.bill = (int)item.bill;
                ord.cardnumber = cnum;
                ord.expiredate = expdate;
                ord.cardcode =Convert.ToInt32(code);
                db.ordertables.Add(ord);
                db.SaveChanges();
               
            }
            
            
            TempData["msg"] = "Transaction Completed...";
            TempData.Keep();

            

            return RedirectToAction("testimo");
        }

        public ActionResult invoiceprint()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            int inid =Convert.ToInt32(TempData["invoiceno"]);
           var orders = db.ordertables.Where(x => x.invoiceid == inid).ToList();

            List<inovice> inoka = db.inovices.ToList();
            ViewBag.invoices = from repi in inoka
                               where repi.invoiceid == inid
                           select new { rep = repi};


            return View(orders);




        }

        public ActionResult testdemo()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            int inid = Convert.ToInt32(TempData["invoiceno"]);
            var orders = db.ordertables.Where(x => x.invoiceid == inid).ToList();

            List<inovice> inoka = db.inovices.ToList();
            ViewBag.invoices = from repi in inoka
                               where repi.invoiceid == inid
                               select new { rep = repi };

            return View(orders);
        }

        public ActionResult testimo()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            billVM vm = new billVM();
            var moviesData = new List<MovieVM>();
            var orderdata = new List<orderVM>();
            var invodata = new List<invoiceVM>();
            var usersData = new List<usertbl>();
            int inid = Convert.ToInt32(TempData["invoiceno"]);
            var invo = (from i in db.inovices
                        where i.invoiceid == inid
                        select i).ToList();
            TempData.Keep();
            var order = (from y in db.ordertables
                         where y.invoiceid == inid
                         select y).ToList().FirstOrDefault();
            vm.inotbl = invo;
            vm.ordert = order;

         

            return View(vm);
        }
        
        public ActionResult done()
        {
            TempData.Remove("tempcart");
            TempData.Remove("total");
            return View();
        }


        public ActionResult mapLocation(int movieId)
        {
            ViewBag.MovieId = movieId;
            using (var context = new moviedetailsdb1())
            {
                ViewBag.MovieName = context.Movie_Item.Where(m => m.movie_id == movieId).Select(m => m.Movie_name).FirstOrDefault();

            }
            moviedetailsdb1 db = new moviedetailsdb1();
            List<movieshop> ms = new List<movieshop>();
            var shopdata = from l in db.Landmark121
                           join mms in ms on l.shopid equals mms.shopid
                           where l.MovieId == movieId
                           select new { shopname = mms.shopname };

            /*  ViewBag.shopdata = shopdata.ToList();*/
            ViewBag.shopdata = db.Landmark121.Where(x => x.MovieId == movieId).Select(y => new customshopmodel{ shopname =y.movieshop.shopname,ownername= y.movieshop.ownername ,registredDate=y.movieshop.registredDate}).ToList();
            return View();
        }

        public ActionResult GetMovieLocations(string Currentlat, string Currentlng, int movieId)
        {
            using (var context = new moviedetailsdb1())
            {
                var currentLocation = DbGeography.FromText("POINT( " + Currentlng + " " + Currentlat + " )");

                //var currentLocation = DbGeography.FromText("POINT( 78.3845534 17.4343666 )");

                var places = (from u in context.Landmark121
                              orderby u.GeoLocation.Distance(currentLocation)
                              where u.MovieId == movieId
                              select u).Take(4).Select(x => new RatingApp.Models.Landmark() {Name = x.LandmarkName, lat = x.GeoLocation.Latitude, lng = x.GeoLocation.Longitude, Distance = x.GeoLocation.Distance(currentLocation) });
                var nearschools = places.ToList();

                List<movieshop> ms = new List<movieshop>();
                var shopdata=from l in context.Landmark121
                             join mms in ms on l.shopid equals mms.shopid
                             where l.MovieId== movieId
                             select new {shopname=mms.shopname};
                             
                ViewBag.shopdata=shopdata.ToList();

                return Json(nearschools, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
