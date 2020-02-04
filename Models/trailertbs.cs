using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    [MetadataType(typeof(trailertbMetaData))]
    public partial class trailertb
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }


    public class trailertbMetaData
    {

    }
}