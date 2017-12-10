namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAddresses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Student_RecordBookNumber", "dbo.Students");
            DropIndex("dbo.Addresses", new[] { "Student_RecordBookNumber" });
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Road = c.String(),
                        House = c.String(),
                        Country = c.String(),
                        Student_RecordBookNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Addresses", "Student_RecordBookNumber");
            AddForeignKey("dbo.Addresses", "Student_RecordBookNumber", "dbo.Students", "RecordBookNumber");
        }
    }
}
