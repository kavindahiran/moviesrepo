//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RatingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Landmark121
    {
        public int ID { get; set; }
        public string LandmarkName { get; set; }
        public string Location { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public System.Data.Entity.Spatial.DbGeography GeoLocation { get; set; }
        public Nullable<int> MovieId { get; set; }
        public Nullable<int> shopid { get; set; }
    
        public virtual Movie_Item Movie_Item { get; set; }
        public virtual movieshop movieshop { get; set; }
    }
}
