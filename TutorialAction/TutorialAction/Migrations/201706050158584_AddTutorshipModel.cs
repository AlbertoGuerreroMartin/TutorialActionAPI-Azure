namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTutorshipModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tutorships",
                c => new
                    {
                        tutorshipID = c.Int(nullable: false, identity: true),
                        teacherID = c.String(maxLength: 128),
                        studentID = c.String(maxLength: 128),
                        courseID = c.Int(nullable: false),
                        reserveID = c.Int(),
                        reserved = c.Boolean(nullable: false),
                        tutorshipType = c.Int(nullable: false),
                        motive = c.String(),
                        date = c.String(),
                        hour = c.String(),
                        minutes = c.String(),
                        duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.tutorshipID)
                .ForeignKey("dbo.Courses", t => t.courseID, cascadeDelete: true)
                .ForeignKey("dbo.Reserves", t => t.reserveID)
                .ForeignKey("dbo.AspNetUsers", t => t.studentID)
                .ForeignKey("dbo.AspNetUsers", t => t.teacherID)
                .Index(t => t.teacherID)
                .Index(t => t.studentID)
                .Index(t => t.courseID)
                .Index(t => t.reserveID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tutorships", "teacherID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tutorships", "studentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tutorships", "reserveID", "dbo.Reserves");
            DropForeignKey("dbo.Tutorships", "courseID", "dbo.Courses");
            DropIndex("dbo.Tutorships", new[] { "reserveID" });
            DropIndex("dbo.Tutorships", new[] { "courseID" });
            DropIndex("dbo.Tutorships", new[] { "studentID" });
            DropIndex("dbo.Tutorships", new[] { "teacherID" });
            DropTable("dbo.Tutorships");
        }
    }
}
