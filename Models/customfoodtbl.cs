using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public partial class HallFoodTbl
    {
        public HttpPostedFileBase ImageFile { get; set; }

        public int foodcount { get; set; }
    }
}