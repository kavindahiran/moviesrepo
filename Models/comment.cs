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
    
    public partial class comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public comment()
        {
            this.replies = new HashSet<reply>();
        }
    
        public int cid { get; set; }
        public string commentline { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<bool> like { get; set; }
        public Nullable<int> movieid { get; set; }
        public Nullable<System.DateTime> commentdate { get; set; }
    
        public virtual Movie_Item Movie_Item { get; set; }
        public virtual usertbl usertbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reply> replies { get; set; }
    }
}
