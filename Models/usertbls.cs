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


    }
}