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
    
    public partial class SensorData
    {
        public int DataID { get; set; }
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<int> Data { get; set; }
        public DateTime? Date { get; set; }
    
        public virtual Device Device { get; set; }
    }
}
