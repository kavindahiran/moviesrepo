using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class customizableCastAndMovie
    {


        //movie cast crew id
        public int cast_id { get; set; }

        public string cast_name { get; set; }
        public string cast_profile { get; set; }
        public string crew_details { get; set; }

        public string Movie_name { get; set; }

        public int movie_id { get; set; }

        public string castDiscription { get; set; }
        public Nullable<bool> castawardsnominated { get; set; }
        public Nullable<bool> died { get; set; }
        public string nickname { get; set; }
        public string status { get; set; }
        //castphotgallery

        public int id { get; set; }
        public Nullable<int> castTableid { get; set; }
        public string castIpath { get; set; }
        public byte[] imageC { get; set; }
        public string castimagename { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}