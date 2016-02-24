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
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        AccessProvider pp = new AccessProvider();
        npruessnerEEntities1 db = new npruessnerEEntities1();
        // Custom property
        public int Privilege { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            bool privileged = pp.HavePrivilege(db.ufn_GetUserID(httpContext.User.Identity.Name.ToString()), this.Privilege);
            
            if (privileged)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class AccessProvider
    {
        npruessnerEEntities1 db = new npruessnerEEntities1();

        public void AddPrivilegeToUser(long? id, string permName)
        {
            db.AddPrivilegeToUser(id, permName);
        }

        public void CreatePrivilege(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool DeletePrivilege(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public string[] FindUsersInPrivilege(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllPrivileges()
        {
            throw new NotImplementedException();
        }

        public UserPrivilege[] GetUserPrivileges(long? id)
        {
            int permCount = db.Entities.Count();
            UserPrivilege[] userPriv = new UserPrivilege[permCount];
            Entity[] priv = new Entity[permCount];

            priv = db.Entities.SqlQuery("SELECT * FROM PRIVILEGE").ToArray();

            for (int j = 0; j < permCount; j++)
            {
                if (HavePrivilege(id, priv[j].EntityID))
                {
                    userPriv[j] = new UserPrivilege
                    {
                        Name = priv[j].Name,
                        IsSet = true,
                        Description = priv[j].Description
                    };
                }
                else
                {
                    userPriv[j] = new UserPrivilege
                    {
                        Name = priv[j].Name,
                        IsSet = false,
                        Description = priv[j].Description
                    };
                }
            }
            return userPriv;
        }

        public string[] GetUsersInPrivileges(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool HavePrivilege(long? id, int priv)
        {
            return db.ufn_HavePrivilege(id, priv);
        }

        public void RemovePrivilegeFromUser(long? id, string privName)
        {
            db.RemovePrivilegeFromUser(id, privName);
        }

        public bool PrivilegeExists(string privName)
        {
            throw new NotImplementedException();
        }

        public Entity GetPrivilege(int privilegeID)
        {
            Entity perm = db.Entities.Find(privilegeID);
            return perm;
        }
    }
}