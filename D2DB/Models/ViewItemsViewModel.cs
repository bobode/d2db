using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;

namespace D2DB.Models
{
    public class ViewItemsViewModel
    {
        public List<ViewItemsViewModel.Item> Items { get; set; }
        public string JSON { get; set; }


        public void PrepairForView(string userId)
        {

            var db = new ApplicationDbContext();
            Items = db.Items.Where(x => x.Character.Account.User.Id == userId).Select(x => new ViewItemsViewModel.Item{
                Json = x.Json,
                Character = x.Character.Name,
                Account = x.Character.Account.Name,
                Realm = x.Character.Account.Server +" " +  x.Character.Ladder,
                Id = x.Id.ToString()
            }).ToList();
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JSON = JsonConvert.SerializeObject(this, Formatting.None, jsonSetting);
        }
        public class Item
        {
            public string Json { get; set; }
            public string Id { get; set; }
            public string Character { get; set; }
            public string Account { get; set; }
            public string Realm { get; set; }
        }
    }
}