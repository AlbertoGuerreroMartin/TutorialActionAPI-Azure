namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReserveModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reserves",
                c => new
                    {
                        reserveID = c.Int(nullable: false, identity: true),
                        teacherID = c.String(maxLength: 128),
                        studentID = c.String(maxLength: 128),
                        courseID = c.Int(nullable: false),
                        tutorshipType = c.Int(nullable: false),
                        motive = c.String(),
                        date = c.String(),
                        hour = c.String(),
                    })
                .PrimaryKey(t => t.reserveID)
                .ForeignKey("dbo.Courses", t => t.courseID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.studentID)
                .ForeignKey("dbo.AspNetUsers", t => t.teacherID)
                .Index(t => t.teacherID)
                .Index(t => t.studentID)
                .Index(t => t.courseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserves", "teacherID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reserves", "studentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reserves", "courseID", "dbo.Courses");
            DropIndex("dbo.Reserves", new[] { "courseID" });
            DropIndex("dbo.Reserves", new[] { "studentID" });
            DropIndex("dbo.Reserves", new[] { "teacherID" });
            DropTable("dbo.Reserves");
        }
    }
}
