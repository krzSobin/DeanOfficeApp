using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeanOfficeApp.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeanOfficeApp.Contracts.Enrollments;
using DeanOfficeApp.ApiTests;
using Moq;
using DeanOfficeApp.Api.BLL.Enrollments;

namespace DeanOfficeApp.Api.Controllers.Tests
{
    [TestClass()]
    public class EnrollmentsControllerTests
    {
        [TestMethod()]
        public void PostEnrollmentForBadStudentTest()
        {
            EnrollmentService service = new EnrollmentService(MockCreator.getEnrollmentMock().Object, MockCreator.GetMockStudents().Object, MockCreator.getLectureMock().Object);
            var result = service.AddEnrollment(1, 1);
            if (result.Error == "Lecture minimal semester is too high")
                Assert.IsTrue(true);
            else
                Assert.Fail();
        }

        //[TestMethod()]
        //public void PostEnrollmentForGoodStudentTest()
        //{
        //    EnrollmentService service = new EnrollmentService(MockCreator.getEnrollmentMock().Object, MockCreator.GetMockStudents().Object, MockCreator.getLectureMock().Object);
        //    var result = service.AddEnrollment(3, 1);
        //    if (result.Created)
        //        Assert.IsTrue(true);
        //    else
        //        Assert.Fail();
        //}
    }
}