using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using D2DB.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace D2DB.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var db = new D2DB.Models.ApplicationDbContext();
             ApplicationUser  user = null;
             var vm = new D2DB.Models.homeIndexViewModel();
             if (User.Identity.IsAuthenticated)
             {
                 var userid = User.Identity.GetUserId();
                 user = db.Users.FirstOrDefault(x => x.Id == userid);
                 vm.User = user;
             }
           
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(homeIndexViewModel inputvm)
        {
            var db = new D2DB.Models.ApplicationDbContext();
            ApplicationUser user = null;
            var vm = new D2DB.Models.homeIndexViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                user = db.Users.FirstOrDefault(x => x.Id == userid);
                if (inputvm != null)
                {
                    user.DisplayName = inputvm.User.DisplayName;
                    db.SaveChanges();
                }
                vm.User = user;
            }

            return View(vm);
        }

       
        public ActionResult ViewUser(Guid id)
        {
            var db = new D2DB.Models.ApplicationDbContext();

            var vm = new D2DB.Models.ViewViewModel();
            var user = db.Users.FirstOrDefault(x => x.Id == id.ToString());
            if (user == null)
                return HttpNotFound();

            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                vm.isMyStuff = userid == user.Id;
            }
            vm.Owner = user.DisplayName;
            user.Accounts.ForEach(x => vm.Accounts.Add(x));
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            vm.JSON = JsonConvert.SerializeObject(vm, Formatting.None, jsonSetting);
            return View("View", vm);
        }
        public ActionResult ViewAccount(Guid id)
        {
            var db = new D2DB.Models.ApplicationDbContext();
           
            var vm = new D2DB.Models.ViewViewModel();
            var user = db.Users.FirstOrDefault(x => x.Accounts.Any(a => a.Id == id));
            if (user == null)
                return HttpNotFound();

            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                vm.isMyStuff = userid == user.Id;
            }
            vm.Owner = user.DisplayName;
            vm.Accounts.Add(user.Accounts.FirstOrDefault(x => x.Id == id));
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            vm.JSON = JsonConvert.SerializeObject(vm, Formatting.None, jsonSetting);
            return View("View", vm);
        }
        public ActionResult ViewChar(Guid id)
        {
            var vm = new ViewViewModel();
            var db = new ApplicationDbContext();
            var car = db.Characters.FirstOrDefault(x => x.Id == id);
            if(car == null)
                return HttpNotFound();
            
            var owner = db.Users.FirstOrDefault(x => x.Accounts.Any(a => a.Characters.Any(c => c.Id == id)));
            if (owner == null)
                return HttpNotFound();
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.Identity.GetUserId();
                vm.isMyStuff = userid == owner.Id;
            }
            vm.Accounts.Add(new Account {Name ="" } );
            vm.Accounts.First().Characters.Add(car);
            vm.Owner = owner.DisplayName;
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            vm.JSON = JsonConvert.SerializeObject(vm, Formatting.None, jsonSetting);

            return View("View",vm);
        }
        public ActionResult View(ViewViewModel vm)
        {
            return View("view", vm);
        }


        public ActionResult DeleteChar(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                  var db = new ApplicationDbContext();
                var userid = User.Identity.GetUserId();
                var car = db.Characters.FirstOrDefault(x => x.Id == id);
                if (db.Users.Any(x => x.Id == userid && x.Accounts.Any(a => a.Characters.Any(c => c.Id == id))))
                {   
                    db.Items.RemoveRange(car.Items);
                    db.Characters.Remove(car);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            return HttpNotFound();
        }
        public ActionResult ViewItems()
        {

            if (User.Identity.IsAuthenticated)
            {
               
                var userid = User.Identity.GetUserId();

                ViewItemsViewModel vm = new ViewItemsViewModel();
                vm.PrepairForView(userid);

                return View(vm);
            }
            return HttpNotFound();
        }

    }
}