namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLoginPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Logins");
            DropColumn("dbo.Logins", "apiKey");
            AddColumn("dbo.Logins", "loginID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Logins", "apiKey", c => c.String(maxLength: 32));
            AddPrimaryKey("dbo.Logins", "loginID");
            CreateIndex("dbo.Logins", "apiKey", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Logins", new[] { "apiKey" });
            DropPrimaryKey("dbo.Logins");
            AlterColumn("dbo.Logins", "apiKey", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Logins", "loginID");
            AddPrimaryKey("dbo.Logins", "apiKey");
        }
    }
}
