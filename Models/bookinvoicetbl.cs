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
    
    public partial class bookinvoicetbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bookinvoicetbl()
        {
            this.bookordertbls = new HashSet<bookordertbl>();
        }
    
        public int bookinvoiceid { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<System.DateTime> orderdate { get; set; }
        public Nullable<double> totalbill { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    
        public virtual usertbl usertbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookordertbl> bookordertbls { get; set; }
    }
}
