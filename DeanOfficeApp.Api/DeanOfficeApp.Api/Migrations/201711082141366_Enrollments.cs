namespace DeanOfficeApp.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Enrollments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnrollmentDate = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                        LectureId = c.Int(nullable: false),
                        Student_RecordBookNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lectures", t => t.LectureId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_RecordBookNumber)
                .Index(t => t.LectureId)
                .Index(t => t.Student_RecordBookNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "Student_RecordBookNumber", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "LectureId", "dbo.Lectures");
            DropIndex("dbo.Enrollments", new[] { "Student_RecordBookNumber" });
            DropIndex("dbo.Enrollments", new[] { "LectureId" });
            DropTable("dbo.Enrollments");
        }
    }
}
