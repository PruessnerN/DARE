//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DARE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int EventID { get; set; }
        public int ThingID { get; set; }
        public string Action { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Thing Thing { get; set; }
    }
}
