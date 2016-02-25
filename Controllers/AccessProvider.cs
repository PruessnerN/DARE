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

        public void AddAccessToUser(long? id, int entID)
        {
            db.GiveEntityAccess(id, entID);
        }

        public string[] FindUsersInAccess(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public UserAccess[] GetUserAccess(long? id)
        {
            Entity[] userEntityArray = db.Entities.Where(x => x.Users.Any(e => e.UserID == id)).ToArray();
            Entity[] allEntities = GetAllEntities();
            UserAccess[] userAccessArray = new UserAccess[allEntities.Count()];
            int n = 0;
            while (n < allEntities.Count())
            {
                foreach (var entity in allEntities)
                {
                    if (userEntityArray.Contains(entity))
                    {
                        userAccessArray[n] = new UserAccess
                        {
                            EntityID = entity.EntityID,
                            Name = entity.Name,
                            Description = entity.Description,
                            IsSet = true
                        };
                        n++;
                    }
                    else
                    {
                        userAccessArray[n] = new UserAccess
                        {
                            EntityID = entity.EntityID,
                            Name = entity.Name,
                            Description = entity.Description,
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

        public bool HaveAccess(long? id, int entID)
        {
            return db.ufn_HaveAccess(id, entID);
        }

        public void RemoveAccessFromUser(long? id, int entID)
        {
            db.RemoveEntityAccess(id, entID);
        }

        public bool AccessExists(string privName)
        {
            throw new NotImplementedException();
        }

        public Entity[] GetAllEntities()
        {
            Entity[] allEntities = db.Entities.ToArray();
            return allEntities;
        }
    }
}