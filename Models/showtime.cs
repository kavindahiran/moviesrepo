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
    
    public partial class showtime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public showtime()
        {
            this.tickettbls = new HashSet<tickettbl>();
        }
    
        public int showid { get; set; }
        public Nullable<int> hallid { get; set; }
        public Nullable<System.TimeSpan> showtime1 { get; set; }
    
        public virtual halltable halltable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tickettbl> tickettbls { get; set; }
    }
}
