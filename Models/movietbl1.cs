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
    using System.Web;

    public partial class movietbl1
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> ratings { get; set; }
        public string imgpath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
