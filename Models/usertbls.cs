using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    [MetadataType(typeof(usertblMetaData))]
    public partial class usertbl
    {
        
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class usertblMetaData
    {

        [Required(ErrorMessage = "Field cannot be blank")]
        public string username { get; set; }

        [Required(ErrorMessage = "Field cannot be blank")]
        [DataType(DataType.Password)]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        public string password { get; set; }
        [Required(ErrorMessage = "Field cannot be blank")]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confirmpass { get; set; }
        [Required(ErrorMessage = "Field cannot be blank")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Field cannot be blank")]
        public string lastname { get; set; }


        [Required(ErrorMessage = "Field cannot be blank")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Field cannot be blank")]
        public string gender { get; set; }

        
        [Required(ErrorMessage = "Field cannot be blank")]
        public Nullable<System.DateTime> DOB { get; set; }


        //[Required(ErrorMessage = "Field cannot be blank")]
        //public string phonenumber { get; set; }


    }
}