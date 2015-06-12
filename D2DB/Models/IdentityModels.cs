using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Script.Serialization;


namespace D2DB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string DisplayName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; }
        public virtual List<Account> Accounts { get; set; }
        public virtual List<PermItem> PermItems { get; set; }
        public virtual List<ItemGroup> ItemGroups { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("D2DB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<PermItem> PermItems { get; set; }
        public virtual DbSet<ItemGroup> ItemGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Item>()
            //    .HasOptional(a => a.)

            base.OnModelCreating(modelBuilder);
        }
    }
    
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set;  }
        public string Name { get; set; }
        public string Server { get; set; }
        public virtual List<Character> Characters { get; set; }
        [ScriptIgnore]
        public virtual ApplicationUser User { get; set; }
        public Account()
        {
            this.Characters = new List<Character>();
        }

    }
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public string Ladder { get; set; }
        public DateTime? lastUpdate { get; set; }
        [ScriptIgnore]
        public virtual Account Account { get; set; }
        public virtual List<Item> Items { get; set; }
        
        public Character()
        {
            this.Items = new List<Item>();
        }
    }
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ScriptIgnore]
        public Character Character { get; set; }
        public string Json { get; set; }
    }
    public class PermItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Json { get; set; }
        [ScriptIgnore]
        public virtual ApplicationUser Owner { get; set; }
    }
    public class ItemGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual List<PermItem> Items { get; set; }
        public string Name { get; set; }
        [ScriptIgnore]
        public virtual ApplicationUser Owner { get; set; }
        public ItemGroup()
        {
            this.Items = new List<PermItem>();
        }
    }
    public class Log
    {
        public int Id { get; set; }
        public string log { get; set; }
    }
    
}