namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        GradeValueId = c.Int(nullable: false),
                        EnrollementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enrollments", t => t.EnrollementId, cascadeDelete: true)
                .ForeignKey("dbo.GradeValues", t => t.GradeValueId, cascadeDelete: true)
                .Index(t => t.GradeValueId)
                .Index(t => t.EnrollementId);
            
            CreateTable(
                "dbo.GradeValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "GradeValueId", "dbo.GradeValues");
            DropForeignKey("dbo.Grades", "EnrollementId", "dbo.Enrollments");
            DropIndex("dbo.Grades", new[] { "EnrollementId" });
            DropIndex("dbo.Grades", new[] { "GradeValueId" });
            DropTable("dbo.GradeValues");
            DropTable("dbo.Grades");
        }
    }
}
