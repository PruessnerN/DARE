//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace DARE.Models
{

    
    public partial class Device
    {
        public Device()
        {
            this.Events = new HashSet<Event>();
            this.Schedules = new HashSet<Schedule>();
            this.SensorDatas = new HashSet<SensorData>();
        }
    
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ClientCode { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<SensorData> SensorDatas { get; set; }
    }
}
