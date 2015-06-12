namespace D2DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.PermItems",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Json = c.String(),
                        ItemGroup_Id = c.Guid(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemGroups", t => t.ItemGroup_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ItemGroup_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermItems", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ItemGroups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PermItems", "ItemGroup_Id", "dbo.ItemGroups");
            DropIndex("dbo.PermItems", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PermItems", new[] { "ItemGroup_Id" });
            DropIndex("dbo.ItemGroups", new[] { "ApplicationUser_Id" });
            DropTable("dbo.PermItems");
            DropTable("dbo.ItemGroups");
        }
    }
}
