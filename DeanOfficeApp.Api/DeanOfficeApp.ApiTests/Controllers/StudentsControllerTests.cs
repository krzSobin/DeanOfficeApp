using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeanOfficeApp.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeanOfficeApp.Api.DAL;
using Moq;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.ApiTests;
using DeanOfficeApp.Contracts;

namespace DeanOfficeApp.Api.Controllers.Tests
{
    [TestClass()]
    public class StudentsControllerTests
    {
        [TestInitialize]
        public void ini()
        {

        }

        //[TestMethod()]
        //public void GetStudentsTest()
        //{
        //    var mock=MockCreator.GetMockStudents();
        //    Assert.Fail();
        //}

        [TestMethod()]
        public async Task PostStudentAsyncTest()
        {
            var mock = MockCreator.GetMockStudents();
            var loggingMock = MockCreator.getLoggingMock();
            CreateStudentDTO student = new CreateStudentDTO();
            student.Email = "a@a.pl";
            student.FirstName = "Joanna";
            student.LastName = "Wypych";
            student.CurrentSemester = 1;
            student.EnrollmentDate = DateTime.Now;
            student.Pesel = 95062555224;

            StudentsController controller = new StudentsController(mock.Object, loggingMock.Object);

            await controller.PostStudentAsync(student);

            var studentList = controller.GetStudents();
            var isAdded=studentList.Any(stud=>stud.Pesel==student.Pesel);

            Assert.IsTrue(isAdded);
        }
    }
}