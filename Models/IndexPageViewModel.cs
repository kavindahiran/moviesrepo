using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RatingApp.Models
{
    public class IndexPageViewModel
    {
        public List<heroarenatable> heroarenatableList{ get; set; }
        public IPagedList<Movie_Item> movietblList { get; set; }

        public List<trailertb> trailertbList { get; set; }

        public List<Review_Table> reviewtbl { get; set; }

        public List<newsforum> forum { get; set; }
        public IPagedList<movieCastcrew> castlist { get; set; }

        //public List<movieCastcrew> castcrew { get; set; }
        public IndexPageViewModel()
        {
            heroarenatableList = new List<heroarenatable>();
            //movietblList = new PagedList<Movie_Item>(1,2);
            trailertbList = new List<trailertb>();
            reviewtbl = new List<Review_Table>();
            forum = new List<newsforum>();
            //castcrew = new List<movieCastcrew>();
            
        }
    }
}