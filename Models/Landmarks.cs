using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public partial class Landmark
    {

        public string Name { get; set; }

     //   public System.Data.Spatial.DbGeography Geolocation { get; set; }

        public double? lat
        {
            get; set;
        }
        public double? lng
        {
            get;
            set;
        }

        public double? Distance { get; set; }
    }
}