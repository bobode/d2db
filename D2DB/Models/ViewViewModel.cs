using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace D2DB.Models
{
    public class ViewViewModel
    {
        public ViewViewModel()
        {
            this.Accounts = new List<Account>();
        }
        public string Owner { get; set; }
        public List<Account> Accounts { get; set; }
        public bool isMyStuff { get; set; }
        public string JSON { get; set; }
    }
}