using DARE.Models;
using DARE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DARE.Controllers
{
    public class PrivilegeProvider
    {
        npruessnerEEntities db = new npruessnerEEntities();

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
            int permCount = db.Privileges.Count();
            UserPrivilege[] userPriv = new UserPrivilege[permCount];
            Privilege[] priv = new Privilege[permCount];

            priv = db.Privileges.SqlQuery("SELECT * FROM PRIVILEGE").ToArray();

            for (int j = 0; j < permCount; j++)
            {
                if (HavePrivilege(id, priv[j]))
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

        public bool HavePrivilege(long? id, Privilege priv)
        {
            return db.ufn_HavePrivilege(id, priv.PrivilegeID);
        }

        public void RemovePrivilegeFromUser(long? id, string privName)
        {
            db.RemovePrivilegeFromUser(id, privName);
        }

        public bool PrivilegeExists(string privName)
        {
            throw new NotImplementedException();
        }

        public Privilege GetPrivilege(int privilegeID)
        {
            Privilege perm = db.Privileges.Find(privilegeID);
            return perm;
        }
    }
}