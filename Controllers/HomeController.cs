using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DARE.Models;
using DARE.ViewModels;
using Newtonsoft.Json;
using System.Web.Services;

namespace DARE.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private npruessnerEEntities1 db = new npruessnerEEntities1();
        private AccessProvider ap = new AccessProvider();

        public ActionResult Index()
        {
            var userID = db.ufn_GetUserID(HttpContext.User.Identity.Name);
            UserAccess[] userAccess = ap.GetUserAccess(userID);
            List<Thing> thingList = new List<Thing>();
            foreach(var thing in userAccess)
            {
                thingList.Add(db.Things.Find(thing.ThingID));
            }
            ViewBag.ClientList = new List<Client>(db.Clients);
            return View(thingList);
        }

        [HttpPost]
        public string getThings(string clientCode)
        {
            List<Thing> things = db.Things.Where(t => t.Client.ClientCode == clientCode).ToList();
            var json = JsonConvert.SerializeObject(things);
            return json;
        }

    }
}