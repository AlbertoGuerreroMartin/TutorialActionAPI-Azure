namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTimetableEntity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Timetables");
            DropColumn("dbo.Timetables", "reserveID");
            AddColumn("dbo.Timetables", "timetableID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Timetables", "timetableID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timetables", "reserveID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Timetables");
            DropColumn("dbo.Timetables", "timetableID");
            AddPrimaryKey("dbo.Timetables", "reserveID");
        }
    }
}
