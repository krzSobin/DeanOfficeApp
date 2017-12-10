namespace DeanOfficeApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueAndMaxLenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lectures", "Name", c => c.String(maxLength: 255));
            CreateIndex("dbo.Teachers", "Pesel", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teachers", new[] { "Pesel" });
            AlterColumn("dbo.Lectures", "Name", c => c.String());
        }
    }
}
