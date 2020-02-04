using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class movietb1metadata
    {
        public int id { get; set; }
        public string name { get; set; }
        [Display(Name="Movie Ratings")]
        public Nullable<int> ratings { get; set; }
        public string imgpath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}