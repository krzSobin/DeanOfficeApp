namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moremodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        LectureId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Bibliography = c.String(),
                        EcstsPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LectureId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false),
                        Degree = c.String(),
                        Position = c.String(),
                        Room = c.String(),
                        EcstsPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teachers");
            DropTable("dbo.Lectures");
        }
    }
}
