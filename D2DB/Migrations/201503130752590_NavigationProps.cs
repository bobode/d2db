namespace D2DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NavigationProps : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Accounts", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.ItemGroups", name: "ApplicationUser_Id", newName: "Owner_Id");
            RenameColumn(table: "dbo.PermItems", name: "ApplicationUser_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.Accounts", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.ItemGroups", name: "IX_ApplicationUser_Id", newName: "IX_Owner_Id");
            RenameIndex(table: "dbo.PermItems", name: "IX_ApplicationUser_Id", newName: "IX_Owner_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PermItems", name: "IX_Owner_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.ItemGroups", name: "IX_Owner_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Accounts", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.PermItems", name: "Owner_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.ItemGroups", name: "Owner_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Accounts", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
