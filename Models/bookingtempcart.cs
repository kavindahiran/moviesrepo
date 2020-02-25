using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class bookingtempcart
    {
        public int bookid { get; set; }

        public int hallid { get; set; }
        public string  hallname { get; set; }

        public float ticketprice { get; set; }

        public int ticketqty { get; set; }

        public string hallLocation { get; set; }

        public float bill { get; set; }

        public int foodqty { get; set; }

        public string foodname { get; set; }

        public  float foodprice { get; set; }

    }
}