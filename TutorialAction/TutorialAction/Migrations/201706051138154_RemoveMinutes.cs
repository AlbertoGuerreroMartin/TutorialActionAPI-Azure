namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMinutes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tutorships", "minutes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorships", "minutes", c => c.String());
        }
    }
}
