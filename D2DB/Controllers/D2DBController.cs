using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace D2DB.Controllers
{
    public class D2DBController : Controller
    {
        // GET: D2DB
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateAccount()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            var db = new D2DB.Models.ApplicationDbContext();
                      
            if(json == null)
                return HttpNotFound();

            dynamic input = JsonConvert.DeserializeObject<object>(json);
            string account = input.AccountId;
            string a = input.Items;
            dynamic items = JsonConvert.DeserializeObject<object>(a);
            var user = db.Users.FirstOrDefault(x => x.PublicId.ToString() == account);
            if (user != null)
            {
                string accountName = input.Account == null ? "Single Player" : input.Account;
                var myd2Account = user.Accounts.FirstOrDefault(x => x.Name == accountName);
                if(myd2Account == null)
                {
                    myd2Account = new D2DB.Models.Account();
                    user.Accounts.Add(myd2Account);
                    myd2Account.Name = accountName;
                    myd2Account.Server = input.Realm;
                }
                string charName = input.CharName;
                string charLad = input.Ladder;
                string chartype = input.Class;
                string charLvl = input.Level;
                var car = myd2Account.Characters.FirstOrDefault(x => x.Name == charName);
                if(car == null)
                {
                    car = new D2DB.Models.Character();
                    car.Name = input.CharName;
                    myd2Account.Characters.Add(car);
                }
                var oldItems = car.Items.ToList();
                
                
                car.Items.Clear();
                foreach (var it in items)
                {
                    var item = new D2DB.Models.Item();
                    item.Json = JsonConvert.SerializeObject(it);
                    var oldIt = oldItems.FirstOrDefault(x => x.Json == item.Json);
                    if (oldIt != null)
                    {
                        car.Items.Add(oldIt);
                    }
                    else
                        car.Items.Add(item);
                }
                car.lastUpdate = DateTime.Now;
                car.Ladder = charLad;
                car.Level = charLvl == "" ? 0 : int.Parse(charLvl);
                car.Class = chartype;
                db.SaveChanges();

                var leftovers = db.Items.Where(x => x.Character == null);
                db.Items.RemoveRange(leftovers);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return HttpNotFound();
        }

    }
}