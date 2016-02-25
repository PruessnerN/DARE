﻿using DARE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DARE.ViewModels
{
    public class UserAccess
    {
        public int EntityID { get; set; }
        public string Name { get; set; }
        public bool IsSet { get; set; }
        public string Description { get; set; }
    }

    public class UserAccessViewModel
    {
        public UserAccess[] UserAccessArray { get; set; }
        public IEnumerable<SelectListItem> AllEntities { get; set; }
        public User User { get; set; }
    }

    public class EntityOptionList
    {
        public static List<SelectListItem> options = new List<SelectListItem>()
        {
            new SelectListItem { Value = "true", Text = "Allow" },
            new SelectListItem { Value = "false", Text = "Deny" }
        };
    }
}