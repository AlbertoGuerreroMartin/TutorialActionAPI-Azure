namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        firstname = c.String(),
                        lastname = c.String(),
                        email = c.String(),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
