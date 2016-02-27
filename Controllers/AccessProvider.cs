using DARE.Models;
using DARE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DARE.Controllers
{
    public class AccessProvider
    {
        npruessnerEEntities1 db = new npruessnerEEntities1();

        public void AddAccessToUser(long? id, int thingID)
        {
            db.GiveThingAccess(id, thingID);
        }

        public string[] FindUsersInAccess(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public UserAccess[] GetUserAccess(long? id)
        {
            Thing[] userThingArray = db.Things.Where(x => x.Users.Any(e => e.UserID == id)).ToArray();
            Thing[] allThings = GetAllThings();
            UserAccess[] userAccessArray = new UserAccess[allThings.Count()];
            int n = 0;
            while (n < allThings.Count())
            {
                foreach (var thing in allThings)
                {
                    if (userThingArray.Contains(thing))
                    {
                        userAccessArray[n] = new UserAccess
                        {
                            ThingID = thing.ThingID,
                            Name = thing.Name,
                            Description = thing.Description,
                            IsSet = true
                        };
                        n++;
                    }
                    else
                    {
                        userAccessArray[n] = new UserAccess
                        {
                            ThingID = thing.ThingID,
                            Name = thing.Name,
                            Description = thing.Description,
                            IsSet = false
                        };
                        n++;
                    }
                }
            }
            return userAccessArray;
        }

        public string[] GetUsersInAccess(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool HaveAccess(long? id, int thingID)
        {
            return db.ufn_HaveAccess(id, thingID);
        }

        public void RemoveAccessFromUser(long? id, int thingID)
        {
            db.RemoveThingAccess(id, thingID);
        }

        public bool AccessExists(int thingID)
        {
            throw new NotImplementedException();
        }

        public Thing[] GetAllThings()
        {
            Thing[] allThings = db.Things.ToArray();
            return allThings;
        }
    }
}