using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    [MetadataType(typeof(usertblMetaData))]
    public class ordermetaclass
    {

    }

    public class ordermetadata
    {
        [Required(ErrorMessage = "Field cannot be blank")]
        public string cardnumber { get; set; }

        [Required(ErrorMessage = "Field cannot be blank")]
        public Nullable<System.DateTime> expiredate { get; set; }

        [Required(ErrorMessage = "Field cannot be blank")]
        public Nullable<int> cardcode { get; set; }
    }
}