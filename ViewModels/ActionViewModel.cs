using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DARE.ViewModels
{
    public class ActionViewModel
    {
        public int ActionID { get; set; }
        public Nullable<int> ThingID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}