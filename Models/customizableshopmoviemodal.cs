using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class customizableshopmoviemodal
    {


        public int movie_id { get; set; }
        public string Movie_name { get; set; }

        public int shopid { get; set; }
        public Nullable<int> movieItemDis { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<bool> isavailable { get; set; }
        public Nullable<double> PreviousPrice { get; set; }
        public Nullable<int> quantityavail { get; set; }
        public string coverphotopath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}