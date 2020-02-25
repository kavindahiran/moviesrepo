using RatingApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class showingmodelcollection
    {
        public List<nowshowing> ns;
        public List<halltable> ht;
        public List<hallLocation> hl;
        public BookNowVM booking = new BookNowVM();
        public customshowmodel cm;
        public List<HallFoodTbl> food;
        public foodordercustommodel foodmodel = new foodordercustommodel();


    }
}