using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models.billviewmodel
{
    public class invoiceVM
    {
        public int invoiceid { get; set; }

        public Nullable<int> totalbill { get; set; }
        public string fullname { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public Nullable<int> zipcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}