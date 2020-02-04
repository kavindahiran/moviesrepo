using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RatingApp.Models;
using System.Data.Entity;
using System.Web.Routing;

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

            shop.movieitems = it;
            shoppingcart sc = db.shoppingcarts.Where(x => x.movieItemDis == movie_id).SingleOrDefault();


            List<reply> rep = db.replies.ToList();
            List<comment> comi = db.comments.ToList();
            shop.cart = sc;

            List<comment> com = db.comments.Include(y=>y.replies).Where(x=>x.movieid==movie_id).ToList();
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
            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public ActionResult postcomment(string cmtbox)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            comment com = new comment();
            return View();

        }
    }
}
