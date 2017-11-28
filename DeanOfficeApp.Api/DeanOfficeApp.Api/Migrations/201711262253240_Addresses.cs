namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addresses : DbMigration
    {
        public override void Up()
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
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Students", "Address_Id", c => c.Int());
            CreateIndex("dbo.Students", "Address_Id");
            AddForeignKey("dbo.Students", "Address_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Students", new[] { "Address_Id" });
            DropColumn("dbo.Students", "Address_Id");
            DropTable("dbo.Addresses");
        }
    }
}
