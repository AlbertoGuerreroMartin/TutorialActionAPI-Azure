namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimetableEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        reserveID = c.Int(nullable: false, identity: true),
                        teacherID = c.String(maxLength: 128),
                        date = c.String(),
                        hour = c.String(),
                        duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.reserveID)
                .ForeignKey("dbo.AspNetUsers", t => t.teacherID)
                .Index(t => t.teacherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timetables", "teacherID", "dbo.AspNetUsers");
            DropIndex("dbo.Timetables", new[] { "teacherID" });
            DropTable("dbo.Timetables");
        }
    }
}
