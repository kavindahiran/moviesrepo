//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RatingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bookordertbl
    {
        public int bookorderid { get; set; }
        public Nullable<int> movieid { get; set; }
        public Nullable<int> invoiceid { get; set; }
        public Nullable<int> hallid { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<double> bill { get; set; }
        public Nullable<double> unitprice { get; set; }
        public string cardnumber { get; set; }
        public Nullable<System.DateTime> expiredate { get; set; }
        public Nullable<int> cardcode { get; set; }
    
        public virtual bookinvoicetbl bookinvoicetbl { get; set; }
        public virtual halltable halltable { get; set; }
        public virtual nowshowing nowshowing { get; set; }
    }
}