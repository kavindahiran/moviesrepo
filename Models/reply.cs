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
    
    public partial class reply
    {
        public int replyid { get; set; }
        public Nullable<int> commentid { get; set; }
        public string replystatus { get; set; }
        public Nullable<System.DateTime> replytime { get; set; }
        public Nullable<int> useridR { get; set; }
    
        public virtual comment comment { get; set; }
        public virtual usertbl usertbl { get; set; }
    }
}