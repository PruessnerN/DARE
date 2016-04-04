﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace DARE.Models
{
    public partial class npruessnerEEntities1
    {
        [DbFunction("npruessnerEModel.Store", "ufn_GetSalt")]
        public byte[] ufn_GetSalt(string username)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("Username", username));
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            var output = lObjectContext.CreateQuery<byte[]>("npruessnerEModel.Store.ufn_GetSalt(@Username)", parameters.ToArray()).Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }

        [DbFunction("npruessnerEModel.Store", "ufn_GetRoleForUser")]
        public int ufn_GetRoleForUser(string username)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("Username", username));
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            var output = lObjectContext.CreateQuery<int>("npruessnerEModel.Store.ufn_GetRoleForUser(@Username)", parameters.ToArray()).Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }

        [DbFunction("npruessnerEModel.Store", "ufn_GetUserID")]
        public long? ufn_GetUserID(string username)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("Username", username));
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            long? output = lObjectContext.CreateQuery<long?>("npruessnerEModel.Store.ufn_GetUserID(@Username)", parameters.ToArray()).Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }

        [DbFunction("npruessnerEModel.Store", "ufn_HaveUsers")]
        public bool ufn_HaveUsers()
        {
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            bool output = lObjectContext.CreateQuery<bool>("npruessnerEModel.Store.ufn_HaveUsers()").Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }

        [DbFunction("npruessnerEModel.Store", "ufn_HaveAccess")]
        public bool ufn_HaveAccess(long? userid, int thingid)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("UserID", userid));
            parameters.Add(new ObjectParameter("ThingID", thingid));
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            bool output = lObjectContext.CreateQuery<bool>("npruessnerEModel.Store.ufn_HaveAccess(@UserID, @ThingID)", parameters.ToArray()).Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }

        [DbFunction("npruessnerEModel.Store", "ufn_GetUserNewMessages")]
        public int ufn_GetUserNewMessages(long userid)
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();
            parameters.Add(new ObjectParameter("UserID", userid));
            var lObjectContext = ((IObjectContextAdapter)this).ObjectContext;
            int output = lObjectContext.CreateQuery<int>("npruessnerEModel.Store.ufn_GetUserNewMessages(@UserID)", parameters.ToArray()).Execute(MergeOption.NoTracking).FirstOrDefault();
            return output;
        }
    }
}