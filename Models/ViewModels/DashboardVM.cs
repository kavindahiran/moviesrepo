using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models.ViewModels
{
    public class DashboardVM
    {
        public List<usertbl> users { get; set; }

        public List<MovieVM> movies { get; set; }

        public List<ReviewVM> reviews { get; set; }
    }
}