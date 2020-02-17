using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace RatingApp.Models.ViewModels
{
    public class ChartVM
    {

        public string name { get; set; }
        public int? y { get; set; }
    }
}