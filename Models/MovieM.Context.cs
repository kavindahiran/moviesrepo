﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class moviedetailsdb1 : DbContext
    {
        public moviedetailsdb1()
            : base("name=moviedetailsdb1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<heroarenatable> heroarenatables { get; set; }
        public virtual DbSet<Movie_Item> Movie_Item { get; set; }
        public virtual DbSet<newsforum> newsforums { get; set; }
        public virtual DbSet<trailertb> trailertbs { get; set; }
        public virtual DbSet<usertbl> usertbls { get; set; }
        public virtual DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public virtual DbSet<shoppingcart> shoppingcarts { get; set; }
        public virtual DbSet<castphotogallery> castphotogalleries { get; set; }
        public virtual DbSet<Landmark121> Landmark121 { get; set; }
        public virtual DbSet<inovice> inovices { get; set; }
        public virtual DbSet<ordertable> ordertables { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<reply> replies { get; set; }
        public virtual DbSet<castratingsT> castratingsTs { get; set; }
        public virtual DbSet<castComment> castComments { get; set; }
        public virtual DbSet<castreply> castreplies { get; set; }
        public virtual DbSet<hallLocation> hallLocations { get; set; }
        public virtual DbSet<nowshowing> nowshowings { get; set; }
        public virtual DbSet<showtime> showtimes { get; set; }
        public virtual DbSet<halltable> halltables { get; set; }
        public virtual DbSet<tickettbl> tickettbls { get; set; }
        public virtual DbSet<movieCastcrew> movieCastcrews { get; set; }
        public virtual DbSet<Review_Table> Review_Table { get; set; }
    }
}
