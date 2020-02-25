using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models.ViewModels
{
    public class BookNowVM
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}