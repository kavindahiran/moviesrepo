using RatingApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models.billviewmodel
{
    public class billVM
    {

        public List<usertbl> users { get; set; }

        public List<MovieVM> movies { get; set; }

        public ordertable ordert { get; set; }

        public List<inovice> inotbl { get; set; }
    }
}