using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DARE.ViewModels
{
    public class ThingViewModel
    {
        public int ThingID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StateDescriptor { get; set; }
    }
}