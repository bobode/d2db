namespace D2DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "Ladder", c => c.String());
            AddColumn("dbo.Characters", "lastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Characters", "lastUpdate");
            DropColumn("dbo.Characters", "Ladder");
        }
    }
}
