﻿using PayPal.Api;
using RatingApp.Models;
using RatingApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingApp.Controllers
{
   
    public class BookticketsController : Controller
    {
        float x = 0;
        int hallidv;
        int movieidv;
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

        public ActionResult selectTime(int? id)
        {
            return View();
        }
        List<bookingtempcart> carty = new List<bookingtempcart>();
        public ActionResult newtime(int? id, BookNowVM bookingData)
        {
            List<bookingtempcart> tc1 = TempData["tempcart2"] as List<bookingtempcart>;
            moviedetailsdb1 db = new moviedetailsdb1();
            List<showtime> sh = db.showtimes.Where(x=>x.hallid==bookingData.HallId).ToList();
            List<halltable> hall = db.halltables.ToList();
            List<hallLocation> loc = db.hallLocations.ToList();
            bookingtempcart tc = new bookingtempcart();
            tc.bookid = bookingData.MovieId;
            tc.hallname = hall.Where(x => x.hallid == bookingData.HallId).FirstOrDefault().hallname;
            tc.ticketprice =(float)(bookingData.Price);
            tc.ticketqty = bookingData.Quantity;
            tc.hallid = bookingData.HallId;
           tc.hallLocation= loc.Where(x => x.locationid == bookingData.LocationId).FirstOrDefault().locationname;
            tc.bill = tc.ticketprice;
            ViewBag.temphallid = bookingData.HallId;
            hallidv = bookingData.HallId;
            ViewBag.ticketprice = tc.bill;
            if (TempData["tempcart2"] == null)
            {
                carty.Add(tc);
                TempData["tempcart2"] = carty;
            }
            else
            {
                List<bookingtempcart> carty2 = TempData["tempcart2"] as List<bookingtempcart>;
                int flag = 0;
                foreach (var item in carty2)
                {
                    if (item.bookid == tc.bookid)
                    {
                        item.ticketqty += tc.ticketqty;
                        item.bill += tc.bill;
                        flag = 1;

                    }
                   
                }
              
                if (flag == 0)
                {
                    carty2.Add(tc);
                }

                TempData["tempcart2"] = carty2;
            }
            TempData.Keep();
            return View(sh);
        }

        [HttpGet]
        public JsonResult GetHallsForMovie(int movieId)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var list = db.halltables.Where(m => m.movieShid == movieId).Select(m => new { m.hallid, m.hallname }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
            //ViewBag.hallname = new SelectList(halls, "hallid", "hallname");
        }

        [HttpGet]
        public JsonResult GetHallLocation(int hallId)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var list = db.hallLocations.Where(m => m.hallid == hallId).Select(m => new { m.locationid, m.locationname }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
            //ViewBag.hallname = new SelectList(halls, "hallid", "hallname");
        }

        [HttpGet]
        public ActionResult GetPrice(int hallId, int quantity)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            var price = db.halltables.Where(m => m.hallid == hallId).Select(m => m.ticketprice).FirstOrDefault();
            price = price * quantity;
            return Json(price, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BookNow(BookNowVM bookingData)
        {
            return View();
        }
        public ActionResult selectseats()
        {
            return View();
        }

        public ActionResult addfooditems(int? id)
        {
            if (TempData["tempcart2"] != null)
            {

                List<bookingtempcart> tc = TempData["tempcart2"] as List<bookingtempcart>;
                foreach (var item in tc)
                {
                    x += item.bill;
                }
                TempData["total2"] = x;

            }
            TempData.Keep();
            moviedetailsdb1 db = new moviedetailsdb1();
            showingmodelcollection model = new showingmodelcollection();
            model.food = db.HallFoodTbls.Where(x=>x.hallid==id).ToList();
            return View(model);
        }

        public ActionResult foodorder(int id,HallFoodTbl food)
        {
            moviedetailsdb1 db = new moviedetailsdb1();

            if (TempData["tempcart2"] != null)
            {

                List<bookingtempcart> tc = TempData["tempcart2"] as List<bookingtempcart>;
                foreach (var item in tc)
                {
                    hallidv = item.hallid;
                    movieidv = item.bookid;
                }
                TempData["total2"] = x;

            }
            TempData.Keep();


            HallFoodTbl it = db.HallFoodTbls.Where(x => x.foodID ==id).SingleOrDefault();
            bookingtempcart cart = TempData["tempcart2"] as bookingtempcart;
            halltable halltb1 = db.halltables.Where(x => x.hallid == hallidv).SingleOrDefault();
           // nowshowing ns = db.nowshowings.Where(x => x.id == movieidv).SingleOrDefault();

            foodordercustommodel shop = new foodordercustommodel();
            int userid = Convert.ToInt32(Session["id"].ToString());
            shop.foodsingle = it;
          //  shop.showing = ns;
            shop.hallt = halltb1;
            return View(shop);
        }
        [HttpPost]
        public ActionResult foodorder(foodordercustommodel model, int qty )
        {
            return View();
        }

        public ActionResult registershops()
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            List<movieshop> ms = db.movieshops.ToList();
            return View(ms);
        }

        public ActionResult orderfood()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = Models.PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }


    }
}