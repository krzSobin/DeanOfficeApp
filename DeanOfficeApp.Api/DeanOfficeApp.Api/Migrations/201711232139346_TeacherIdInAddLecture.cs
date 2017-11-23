namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherIdInAddLecture : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Lectures", new[] { "TeacherId" });
            AlterColumn("dbo.Lectures", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lectures", "TeacherId");
            AddForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Lectures", new[] { "TeacherId" });
            AlterColumn("dbo.Lectures", "TeacherId", c => c.Int());
            CreateIndex("dbo.Lectures", "TeacherId");
            AddForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers", "TeacherId");
        }
    }
}
