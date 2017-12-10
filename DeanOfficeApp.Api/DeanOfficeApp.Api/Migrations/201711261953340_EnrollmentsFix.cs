namespace DeanOfficeApp.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EnrollmentsFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "Student_RecordBookNumber", "dbo.Students");
            DropIndex("dbo.Enrollments", new[] { "Student_RecordBookNumber" });
            DropColumn("dbo.Enrollments", "StudentId");
            RenameColumn(table: "dbo.Enrollments", name: "Student_RecordBookNumber", newName: "StudentId");
            AlterColumn("dbo.Enrollments", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrollments", "StudentId");
            AddForeignKey("dbo.Enrollments", "StudentId", "dbo.Students", "RecordBookNumber", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "StudentId", "dbo.Students");
            DropIndex("dbo.Enrollments", new[] { "StudentId" });
            AlterColumn("dbo.Enrollments", "StudentId", c => c.Int());
            RenameColumn(table: "dbo.Enrollments", name: "StudentId", newName: "Student_RecordBookNumber");
            AddColumn("dbo.Enrollments", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrollments", "Student_RecordBookNumber");
            AddForeignKey("dbo.Enrollments", "Student_RecordBookNumber", "dbo.Students", "RecordBookNumber");
        }
    }
}
