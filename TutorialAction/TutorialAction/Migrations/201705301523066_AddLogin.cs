namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Login",
                c => new
                {
                    apiKey = c.Int(),
                    userID = c.Int(nullable: false, identity: true)
                })
                .PrimaryKey(t => t.apiKey)
                .ForeignKey("dbo.Users", t => t.userID);
        }

        public override void Down()
        {
            DropTable("dbo.Login");
        }
    }
}
