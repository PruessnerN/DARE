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
    
    public partial class Schedule
    {
        public int ScheduleID { get; set; }
        public int ThingID { get; set; }
        public Nullable<int> ActionID { get; set; }
        public string Name { get; set; }
        public string CronExpression { get; set; }
        public string Description { get; set; }
    
        public virtual Thing Thing { get; set; }
        public virtual Action Action { get; set; }
    }
}
