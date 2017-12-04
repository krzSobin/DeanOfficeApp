using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.DAL.Logging;
using DeanOfficeApp.Api.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.ApiTests
{
    public class MockCreator
    {
        private static MockCreator _instance;
        private MockCreator()
        {
            Student[] students = new Student[]
            {
                new Student{ RecordBookNumber = 271102, Pesel = 95062500222, CurrentSemester = 1, EnrollmentDate = DateTime.Now},
                new Student{ RecordBookNumber = 271103, Pesel = 95062522222, CurrentSemester = 2, EnrollmentDate = DateTime.Now},
                new Student{ RecordBookNumber = 271104, Pesel = 95062650222, CurrentSemester = 3, EnrollmentDate = DateTime.Now}
            };

            mock.Setup(m => m.GetStudents("")).Returns(students);
        }
        private Mock<IStudentRepository> mock = new Mock<IStudentRepository>();
        private Mock<ILoggingRepository> loggingMock = new Mock<ILoggingRepository>();

        public static Mock<IStudentRepository> GetMockStudents()
        {
            if (_instance == null)
                _instance = new MockCreator();
            return _instance.getMockStudent();

        }
        public static Mock<ILoggingRepository> getLoggingMock()
        {
            if (_instance == null)
                _instance = new MockCreator();
            return _instance.getMockLogging();
        }

        private Mock<IStudentRepository> getMockStudent()
        {
            return mock;
        }

        private Mock<ILoggingRepository> getMockLogging()
        {
            return loggingMock;
        }
    }
}
