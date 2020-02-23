using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    [MetadataType(typeof(shoptblMetaData))]
    public partial class movieshop
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }


    public class shoptblMetaData
    {
        [Required(ErrorMessage = "Required")]
        public string shopname { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ownername { get; set; }
       
        public Nullable<System.DateTime> registredDate { get; set; }
        public Nullable<System.DateTime> establisheddate { get; set; }
        [Required(ErrorMessage = "Required")]
        public string address1 { get; set; }
        [Required(ErrorMessage = "Required")]
        public string address2 { get; set; }
        [Required(ErrorMessage = "Required")]
        public string landno { get; set; }
        [Required(ErrorMessage = "Required")]
        public string mobileno { get; set; }
        [Required(ErrorMessage = "Required")]
        public string fax { get; set; }
        [Required(ErrorMessage = "Required")]
        public string country { get; set; }
        [Required(ErrorMessage = "Required")]
        public string state { get; set; }
        [Required(ErrorMessage = "Required")]
        public string city { get; set; }
        [Required(ErrorMessage = "Required")]
        public string zipcode { get; set; }

    }
}