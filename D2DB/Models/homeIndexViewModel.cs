using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using D2DB.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace D2DB.Models
{
    public class homeIndexViewModel 
    {
        public ApplicationUser User { get; set; }

    }
    
}