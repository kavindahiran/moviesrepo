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
    
    public partial class nowshowing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nowshowing()
        {
            this.halltables = new HashSet<halltable>();
            this.tickettbls = new HashSet<tickettbl>();
            this.bookordertbls = new HashSet<bookordertbl>();
            this.foodOrderTbls = new HashSet<foodOrderTbl>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> availableDate { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public string imagepath { get; set; }
        public byte[] image { get; set; }
        public string moviename { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<halltable> halltables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tickettbl> tickettbls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookordertbl> bookordertbls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<foodOrderTbl> foodOrderTbls { get; set; }
    }
}
