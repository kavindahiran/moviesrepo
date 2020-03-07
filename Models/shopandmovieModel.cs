using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class shopandmovieModel
    {
        public movieshop ms { get; set; }
        public Movie_Item mi { get; set; }

       public List<Landmark121> land { get; set; }
       
        public int shopid { get; set; }

        public int movie_id { get; set; }

        public int moviecount { get; set; }

        public int coupencount { get; set; }
    }
}