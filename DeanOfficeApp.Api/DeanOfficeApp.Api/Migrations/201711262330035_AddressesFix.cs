namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressesFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Students", new[] { "Address_Id" });
            AddColumn("dbo.Addresses", "Student_RecordBookNumber", c => c.Int());
            CreateIndex("dbo.Addresses", "Student_RecordBookNumber");
            AddForeignKey("dbo.Addresses", "Student_RecordBookNumber", "dbo.Students", "RecordBookNumber");
            DropColumn("dbo.Addresses", "UserId");
            DropColumn("dbo.Students", "Address_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Address_Id", c => c.Int());
            AddColumn("dbo.Addresses", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Addresses", "Student_RecordBookNumber", "dbo.Students");
            DropIndex("dbo.Addresses", new[] { "Student_RecordBookNumber" });
            DropColumn("dbo.Addresses", "Student_RecordBookNumber");
            CreateIndex("dbo.Students", "Address_Id");
            AddForeignKey("dbo.Students", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
