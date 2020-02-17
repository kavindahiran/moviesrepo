using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models.ViewModels
{
    public class MovieVM
    {
        public int? movieId { get; set; }

        [Display(Name = "Movie Name")]
        public string movieName { get; set; }

        [Display(Name = "Quantity Available")]
        public int? quantityAvailable { get; set; }
        public double? Rating { get; set; }
        public double? Price { get; set; }

        [Display(Name = "Release Date")]
        public string releaseDate { get; set; }
    }
}