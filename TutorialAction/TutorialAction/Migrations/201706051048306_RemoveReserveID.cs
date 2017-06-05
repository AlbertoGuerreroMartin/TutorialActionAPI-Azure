namespace TutorialAction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveReserveID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tutorships", "reserveID", "dbo.Reserves");
            DropIndex("dbo.Tutorships", new[] { "reserveID" });
            DropColumn("dbo.Tutorships", "reserveID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutorships", "reserveID", c => c.Int());
            CreateIndex("dbo.Tutorships", "reserveID");
            AddForeignKey("dbo.Tutorships", "reserveID", "dbo.Reserves", "reserveID");
        }
    }
}
