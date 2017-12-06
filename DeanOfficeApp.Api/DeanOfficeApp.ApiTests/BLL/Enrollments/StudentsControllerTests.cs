using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeanOfficeApp.Api.BLL.Enrollments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeanOfficeApp.Api.Controllers;
using DeanOfficeApp.ApiTests;
using DeanOfficeApp.Api.Models;

namespace DeanOfficeApp.Api.BLL.Enrollments.Tests
{
    [TestClass()]
    public class StudentsControllerTests
    {
        [TestInitialize]
        public void Init()
        {
            Startup.InitForTests();
        }
        [TestMethod()]
        public void AddGradeTest()
        {
            var grade = new Contracts.Grades.AddGradeDTO();
            grade.Comment = "Komentarz do oceny";
            grade.Date = DateTime.Now;
            grade.EnrollementId = 1;
            EnrollmentsController controller = new EnrollmentsController(MockCreator.getEnrollmentMock().Object, MockCreator.GetMockStudents().Object, null);
            controller.AddGrade(1, grade);
            var enrollment = controller.GetEnrollments(1);
            var grades=enrollment.Grades;

            Assert.IsNull(grades.Last().GradeValueId);
        }
    }
}