using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class customcoupenmodel
    {
        public string shopname { get; set; }

        public string ownername { get; set; }

        public string mobileno { get; set; }

        public string shopimage { get; set; }

        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }

        public string address1 { get; set; }

        public string coupenNo { get; set; }

        public string username { get; set; }

        public Nullable<int> shopid { get; set; }
        public Nullable<double> discount { get; set; }
        public Nullable<System.DateTime> genDate { get; set; }
    }
}