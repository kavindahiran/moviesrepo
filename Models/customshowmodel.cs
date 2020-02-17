using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class customshowmodel
    {
        public int id { get; set; }
        public Nullable<System.DateTime> availableDate { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public string imagepath { get; set; }
        public byte[] image { get; set; }
        public string moviename { get; set; }



        public int locationid { get; set; }
        public string locationname { get; set; }
    


        public int hallid { get; set; }
        public string hallname { get; set; }
        public Nullable<int> movieShid { get; set; }
      //  public Nullable<int> movieshowid { get; set; }
    }
}