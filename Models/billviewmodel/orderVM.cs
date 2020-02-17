using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models.billviewmodel
{
    public class orderVM
    {
        public int orderid { get; set; }

        public Nullable<int> prodid { get; set; }
        public Nullable<int> invoiceid { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> bill { get; set; }
        public Nullable<int> unitprice { get; set; }
        public string cardnumber { get; set; }
        public Nullable<System.DateTime> expiredate { get; set; }
        public Nullable<int> cardcode { get; set; }
    }
}