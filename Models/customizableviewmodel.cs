using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class customizableviewmodel
    {

        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> ratings { get; set; }
        public string imgpath { get; set; }

        public int arenaid { get; set; }
        public string Aname { get; set; }
        public Nullable<int> Aratings { get; set; }
        public string description { get; set; }
        public string imagepath { get; set; }


        public HttpPostedFileBase ImageFile { get; set; }

    }
}