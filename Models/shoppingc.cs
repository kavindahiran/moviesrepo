using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public partial class shoppingcart
    {
        public DateTime? dateCreated;

        public Nullable<System.DateTime> publishdate
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}