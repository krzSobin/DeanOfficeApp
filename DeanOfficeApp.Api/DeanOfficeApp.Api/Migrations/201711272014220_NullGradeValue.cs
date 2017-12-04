namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullGradeValue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Grades", "GradeValueId", "dbo.GradeValues");
            DropIndex("dbo.Grades", new[] { "GradeValueId" });
            AlterColumn("dbo.Grades", "GradeValueId", c => c.Int());
            CreateIndex("dbo.Grades", "GradeValueId");
            AddForeignKey("dbo.Grades", "GradeValueId", "dbo.GradeValues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "GradeValueId", "dbo.GradeValues");
            DropIndex("dbo.Grades", new[] { "GradeValueId" });
            AlterColumn("dbo.Grades", "GradeValueId", c => c.Int(nullable: false));
            CreateIndex("dbo.Grades", "GradeValueId");
            AddForeignKey("dbo.Grades", "GradeValueId", "dbo.GradeValues", "Id", cascadeDelete: true);
        }
    }
}
