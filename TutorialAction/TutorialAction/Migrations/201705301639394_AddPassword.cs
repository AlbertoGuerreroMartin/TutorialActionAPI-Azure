namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "password");
        }
    }
}
