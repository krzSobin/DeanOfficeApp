namespace DeanOfficeApp.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TeacherPesel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Pesel", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Pesel");
        }
    }
}
