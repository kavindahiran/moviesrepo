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
    
    public partial class castratingsT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public castratingsT()
        {
            this.castComments = new HashSet<castComment>();
        }
    
        public int castRid { get; set; }
        public Nullable<int> castid { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<double> castratings { get; set; }
        public string comments { get; set; }
        public Nullable<System.DateTime> dateposted { get; set; }
    
        public virtual castratingsT castratingsT1 { get; set; }
        public virtual usertbl usertbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<castComment> castComments { get; set; }
        public virtual movieCastcrew movieCastcrew { get; set; }
    }
}
