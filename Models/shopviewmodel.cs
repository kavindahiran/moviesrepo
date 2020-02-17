using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class shopviewmodel
    {


        public Movie_Item movieitems { get; set; }

        public PhotoGallery photogallery { get; set; }

        public shoppingcart cart { get; set; }

        public Review_Table rv { get; set; }
      
      //  public Review_Table reve { get; set; }

        public List<comment> cm { get; set; }

        public List<reply> rep { get; set; }

    }
}