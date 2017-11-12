namespace DeanOfficeApp.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class LecturesminimumSemester : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lectures", "Teacher_TeacherId", "dbo.Teachers");
            DropIndex("dbo.Lectures", new[] { "Teacher_TeacherId" });
            RenameColumn(table: "dbo.Lectures", name: "Teacher_TeacherId", newName: "TeacherId");
            AddColumn("dbo.Lectures", "MinimalSemester", c => c.Int(nullable: false));
            AlterColumn("dbo.Lectures", "TeacherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lectures", "TeacherId");
            AddForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers", "TeacherId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Lectures", new[] { "TeacherId" });
            AlterColumn("dbo.Lectures", "TeacherId", c => c.Int());
            DropColumn("dbo.Lectures", "MinimalSemester");
            RenameColumn(table: "dbo.Lectures", name: "TeacherId", newName: "Teacher_TeacherId");
            CreateIndex("dbo.Lectures", "Teacher_TeacherId");
            AddForeignKey("dbo.Lectures", "Teacher_TeacherId", "dbo.Teachers", "TeacherId");
        }
    }
}
