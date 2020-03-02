using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RatingApp.Models;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;

namespace RatingApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page, int? page2)
        {
            IndexPageViewModel model = new IndexPageViewModel();
            moviedetailsdb1 db = new moviedetailsdb1();
            model.heroarenatableList = db.heroarenatables.ToList();
            model.movietblList = db.Movie_Item.ToList().ToPagedList(page ?? 1,6);
            model.reviewtbl = db.Review_Table.ToList();
            model.castlist = db.movieCastcrews.ToList().ToPagedList(page2 ?? 1, 6);
            model.trailertbList = db.trailertbs.ToList();
            model.reviewtbl = db.Review_Table.ToList();
            model.forum = db.newsforums.ToList();

            List<Movie_Item> mv = db.Movie_Item.ToList();
            List<Review_Table> rv = db.Review_Table.ToList();

            //ViewBag.sh= from movie in mv
            //            join rev in rv on movie.movie_id equals rev.movieT_ID         
            //            select new shopviewmodel { movieitems = movie,reve=rev};
            
            model.reviewtbl = db.Review_Table.Where(x => x.movieT_ID == x.Movie_Item.movie_id).ToList();
            return View(model);

            /*   List<movietbl1> mv = db.movietbl1.ToList();
               customizableviewmodel cm = new customizableviewmodel();
               List<customizableviewmodel> cusl = mv.Select(x => new customizableviewmodel
               { name = x.name, ratings = x.ratings, imgpath = x.imgpath }).ToList();

               return View(cusl);*/
        }
        [HttpPost]
        public ActionResult Index(string search,int? page)
        {
            IndexPageViewModel model = new IndexPageViewModel();

            moviedetailsdb1 db = new moviedetailsdb1();
            model.movietblList = db.Movie_Item.Where(x => x.Movie_name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 6);
            return PartialView("movietile", model.movietblList);
            /* return View(model);*/
        }


        [HttpPost]
        public ActionResult crewsearch(string search, int? page)
        {
            IndexPageViewModel model = new IndexPageViewModel();

            moviedetailsdb1 db = new moviedetailsdb1();
            model.castlist = db.movieCastcrews.Where(x => x.cast_name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 6);
            return PartialView("crewtile", model.castlist);
        }

            public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult movietitles(string search)
        {
           
            return PartialView("movietitles");
      


        }

        public ActionResult heroArea()
        {

            moviesdb db = new moviesdb();

            var data = (from x in db.movietbl1 select x).ToList();


            return PartialView(data);
        }

        public ActionResult Login(string email, string password)
        {
            moviedetailsdb1 rdb = new moviedetailsdb1();

            usertbl emp = rdb.usertbls.FirstOrDefault(x => x.email == email && x.password == password);
            string result = "fail";
            if (emp != null)
            {
                Session["id"] = emp.user_id;
                Session["regname"] = emp.username;
                Session["profpic"] = emp.profile_pic;
                Session["roleid"] = emp.roleid;

                if (emp.roleid == true)
                {
                    result = "admin";
                    return RedirectToAction("Index");
                }
                else if (emp.roleid == false)
                {
                    result = "user";
                    return RedirectToAction("Index");
                }
                else if (emp.roleid == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.message = "login success";
            }
            else
            {
                return RedirectToAction("loginfailed", "errors");
            }
            return Json(result,JsonRequestBehavior.AllowGet);

        }

        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult register(string regname, string password, string email,HttpPostedFileBase ImageFile)
        {
            // regdb rdb = new regdb();
            moviedetailsdb1 db = new moviedetailsdb1();
            byte[] imagebyte = null;
            usertbl reg = new usertbl();
            reg.ImageFile = ImageFile;
            string fileName = Path.GetFileNameWithoutExtension(reg.ImageFile.FileName);
            string extension = Path.GetExtension(reg.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            reg.profile_pic = "~/movieIcons/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/movieIcons/"), fileName);
            BinaryReader reader = new BinaryReader(ImageFile.InputStream);
            imagebyte = reader.ReadBytes(ImageFile.ContentLength);
            reg.ImageFile.SaveAs(fileName);

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("kavindahiran619@gmail.com");
            mm.To.Add(new MailAddress(email));
            mm.Subject = "Registration to LankanMovies";
            mm.Body = "We highly appreciate on your registration";
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";

            smtp.Port = 587;


            /* NetworkCredential nc = new NetworkCredential("kavindahiran619@gmail.com", "lordbuddha", "smtp.gmail.com");*/
            smtp.Credentials = new System.Net.NetworkCredential("kavindahiran619@gmail.com", "hyqgqtrjhlecwdmo");
            smtp.EnableSsl = true;
            // smtp.UseDefaultCredentials = true;
            //smtp.Credentials = nc;

            smtp.Send(mm);
            ViewBag.mesage = "message sent successfully";


            //regemp reg = new regemp();
            reg.username = regname;
            reg.password = password;
            reg.email = email;
            reg.imagebyte = imagebyte;
            reg.roleid = false;
            db.usertbls.Add(reg);
            db.SaveChanges();

            ModelState.Clear();
            // return RedirectToAction("Index");
            return View();
        }


        [HttpGet]
        public ActionResult register2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register2(usertbl us, HttpPostedFileBase ImageFile, string Gender, DateTime DOB)
        {
            moviedetailsdb1 db = new moviedetailsdb1();
            byte[] imagebyte = null;
            usertbl reg = new usertbl();
            reg.ImageFile = ImageFile;
          
                string fileName = Path.GetFileNameWithoutExtension(reg.ImageFile.FileName);
                string extension = Path.GetExtension(reg.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                reg.profile_pic = "~/movieIcons/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/movieIcons/"), fileName);
                BinaryReader reader = new BinaryReader(ImageFile.InputStream);
                imagebyte = reader.ReadBytes(ImageFile.ContentLength);
                reg.ImageFile.SaveAs(fileName);
            
           
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("kavindahiran619@gmail.com");
                mm.To.Add(new MailAddress(us.email));
                mm.Subject = "Registration to LankanMovies";
                mm.Body = "We highly appreciate on your registration";
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                smtp.Port = 587;


                /* NetworkCredential nc = new NetworkCredential("kavindahiran619@gmail.com", "lordbuddha", "smtp.gmail.com");*/
                smtp.Credentials = new System.Net.NetworkCredential("kavindahiran619@gmail.com", "hyqgqtrjhlecwdmo");
                smtp.EnableSsl = true;
                // smtp.UseDefaultCredentials = true;
                //smtp.Credentials = nc;

                smtp.Send(mm);
                ViewBag.mesage = "message sent successfully";

            try
            {
                if (ModelState.IsValid)
                {
                    //regemp reg = new regemp();
                    reg.username = us.username;
                    reg.password = us.password;
                    reg.email = us.email;
                    reg.firstname = us.firstname;
                    reg.lastname = us.lastname;
                    reg.DOB = DOB;
                    reg.gender = us.gender;
                    reg.confirmpass = us.confirmpass;

                    reg.imagebyte = imagebyte;
                    reg.roleid = false;
                    reg.premiumuser = false;
                    reg.registeredDate = DateTime.Now;
                    db.usertbls.Add(reg);
                    db.SaveChanges();

                }
            }
             catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)  
    {  
        Exception raise = dbEx;  
        foreach (var validationErrors in dbEx.EntityValidationErrors)  
        {  
            foreach (var validationError in validationErrors.ValidationErrors)  
            {  
                string message = string.Format("{0}:{1}",  
                    validationErrors.Entry.Entity.ToString(),  
                    validationError.ErrorMessage);  
                // raise a new exception nesting  
                // the current instance as InnerException  
                raise = new InvalidOperationException(message, raise);  
            }  
        }  
        throw raise;  
    }  
            ModelState.Clear();
            return View();
        }

        public ActionResult showtrailer()
        {
            moviesdb db = new moviesdb();


            var data = (from x in db.movietbl1 select x).ToList();


            return PartialView(data);
          
        }

        public ActionResult shownews()
        {
            moviedetailsdb1 db = new moviedetailsdb1();

            Movie_Item it = new Movie_Item();
          
            var data = (from x in db.newsforums select x).ToList();


            return PartialView(data);
        }

      

    }
}