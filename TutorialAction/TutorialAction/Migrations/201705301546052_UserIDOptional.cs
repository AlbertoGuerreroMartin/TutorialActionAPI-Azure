namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIDOptional : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        apiKey = c.Int(nullable: false, identity: true),
                        userID = c.Int(),
                    })
                .PrimaryKey(t => t.apiKey)
                .ForeignKey("dbo.Users", t => t.userID)
                .Index(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "userID", "dbo.Users");
            DropIndex("dbo.Logins", new[] { "userID" });
            DropTable("dbo.Logins");
        }
    }
}
