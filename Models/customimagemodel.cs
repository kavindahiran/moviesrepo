using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class customimagemodel
    {
        //movie photo id
        public int photoID { get; set; }
        public Nullable<int> moviPhotoId { get; set; }
        public string ipath { get; set; }
        public byte[] binary { get; set; }
        public string movietitle { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

      
      



        public int movie_id { get; set; }
        public string Movie_name { get; set; }


    }
}