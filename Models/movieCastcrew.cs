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
    
    public partial class movieCastcrew
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public movieCastcrew()
        {
            this.Review_Table = new HashSet<Review_Table>();
        }
    
        public int cast_id { get; set; }
        public Nullable<int> movie_id { get; set; }
        public string cast_name { get; set; }
        public string cast_profile { get; set; }
        public string crew_details { get; set; }
        public string castDiscription { get; set; }
    
        public virtual Movie_Item Movie_Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review_Table> Review_Table { get; set; }
    }
}