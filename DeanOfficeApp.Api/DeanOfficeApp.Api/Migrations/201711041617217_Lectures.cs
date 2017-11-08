namespace DeanOfficeApp.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Lectures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lectures", "Teacher_TeacherId", c => c.Int());
            CreateIndex("dbo.Lectures", "Teacher_TeacherId");
            AddForeignKey("dbo.Lectures", "Teacher_TeacherId", "dbo.Teachers", "TeacherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "Teacher_TeacherId", "dbo.Teachers");
            DropIndex("dbo.Lectures", new[] { "Teacher_TeacherId" });
            DropColumn("dbo.Lectures", "Teacher_TeacherId");
        }
    }
}
