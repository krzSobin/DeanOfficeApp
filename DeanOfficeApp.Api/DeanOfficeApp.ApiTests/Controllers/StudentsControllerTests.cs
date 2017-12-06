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
using DeanOfficeApp.Api.BLL;

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
            var userManagerMock = MockCreator.getUserMock();
            CreateStudentDTO student = new CreateStudentDTO();
            student.Email = "a@a.pl";
            student.FirstName = "Joanna";
            student.LastName = "Wypych";
            student.CurrentSemester = 1;
            student.EnrollmentDate = DateTime.Now;
            student.Pesel = 95062555224;

            StudentService service = new StudentService(MockCreator.getUserMock().Object,null,MockCreator.GetMockStudents().Object, null, null, MockCreator.getAdressMock().Object );

            service.AddStudentAsync(student);
          

            Assert.IsTrue(true);
        }
    }
}