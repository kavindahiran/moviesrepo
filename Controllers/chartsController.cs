using RatingApp.Models;
using RatingApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingApp.Controllers
{
    public class chartsController : Controller
    {
        public ActionResult Index()
        {
            var dashboardData = new DashboardVM();

            var moviesData = new List<MovieVM>();
            var chartData = new List<ChartVM>();
            var usersData = new List<usertbl>();
            var reviewsData = new List<ReviewVM>();

            using (var _context = new moviedetailsdb1())
            {
                var aMovieIds = (from s in _context.shoppingcarts
                                 where s.isavailable == true
                                 select s.movieItemDis).ToList();

                foreach (var i in aMovieIds)
                {
                    var movieName = _context.Movie_Item.Where(m => m.movie_id == i).Select(m => m.Movie_name).FirstOrDefault();
                    var quantityAvailable = _context.shoppingcarts.Where(m => m.movieItemDis == i).Select(m => m.quantityavail).FirstOrDefault();
                    var Rating = _context.Review_Table.Where(m => m.movieT_ID == i).Select(m => m.rating).FirstOrDefault();
                    var Price = _context.shoppingcarts.Where(m => m.movieItemDis == i).Select(m => m.price).FirstOrDefault();
                    var ReleaseDate = _context.Movie_Item.Where(m => m.movie_id == i).Select(m => m.Movie_release).FirstOrDefault().Value.ToString("dd-MMM-yyyy");

                    var movieVm = new MovieVM { movieId = i, movieName = movieName, quantityAvailable = quantityAvailable, Price = Price, Rating = Rating, releaseDate = ReleaseDate };
                    moviesData.Add(movieVm);

                    var chartVm = new ChartVM { name = movieName, y = quantityAvailable };
                    chartData.Add(chartVm);

                    reviewsData = _context.Review_Table.GroupBy(info => info.userID)
                        .Select(group => new ReviewVM
                        {
                            UserName = _context.usertbls.Where(m => m.user_id == group.Key).Select(m => m.username).FirstOrDefault(),
                            TotalMoviesRated = group.Select(m => m.movieT_ID).Distinct().Count()
                        }).ToList();

                    usersData = (from u in _context.usertbls select u).ToList();
                }

                ViewBag.DataPoints = chartData;
            }

            dashboardData.movies = moviesData;
            dashboardData.users = usersData;
            dashboardData.reviews = reviewsData;

            return View(dashboardData);
        }
    }
}
