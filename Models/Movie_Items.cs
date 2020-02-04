using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    [MetadataType(typeof(Movie_ItemMetaData))]
    public partial class Movie_Item
    {
        public HttpPostedFileBase ImageFile { get; set; }


        public DateTime? dateCreated;

        public Nullable<System.DateTime> Posted_date
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }


        public class Movie_ItemMetaData
        {

            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> Movie_release { get; set; }
            //public DateTime? dateCreated;

            //public Nullable<System.DateTime> Posted_date {
            //    get { return dateCreated ?? DateTime.Now; }
            //    set { dateCreated = value; }

            //
            /* [Display(Name = "Date Created")]
             public Nullable<System.DateTime> Posted_date
             {
                 get { return DateTime.Now;  }

             }*/
        }
    }
}