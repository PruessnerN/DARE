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
    
    public partial class Note
    {
        public int NoteID { get; set; }
        public long SenderID { get; set; }
        public long ReceiverID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool isNew { get; set; }
        public bool Alert { get; set; }
        public bool PushNotification { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> AlertDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
