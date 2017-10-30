namespace DeanOfficeApp.Api.Migrations
{
    using DeanOfficeApp.Api.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DeanOfficeApp.Api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //using (var manager = new ApplicationUserManager(new CustomUserStore(context)))
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = "user2",
            //        FirstName = "Daro",
            //        LastName = "Daro",
            //        Email = "test2@test.pl",
            //        EmailConfirmed = true,
            //        CreatedDate = DateTime.Now.AddYears(-3)
            //    };
            //    var result = manager.CreateAsync(user, "test123").Result;


            //    var student = new Student
            //    {
            //        CurrentSemester = 4,
            //        EnrollmentDate = DateTime.Now.AddYears(-3),
            //        Pesel = 976349623,
            //        RecordBookNumber = user.Id,
            //        UserId = user.Id,
            //        UserData = user
            //    };

            //    context.Students.Add(student);
            //    context.SaveChanges();
            //}
        }
    }
}
