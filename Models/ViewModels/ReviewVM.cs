using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RatingApp.Models.ViewModels
{
    public class ReviewVM
    {
      
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Display(Name = "Total No. of Movies Rated")]
            public int TotalMoviesRated { get; set; }

        
    }
}