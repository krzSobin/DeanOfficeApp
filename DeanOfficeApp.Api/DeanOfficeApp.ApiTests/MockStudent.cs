using DeanOfficeApp.Api;
using DeanOfficeApp.Api.BLL.Users;
using DeanOfficeApp.Api.DAL;
using DeanOfficeApp.Api.DAL.Enrollments;
using DeanOfficeApp.Api.DAL.Lectures;
using DeanOfficeApp.Api.DAL.Logging;
using DeanOfficeApp.Api.DAL.User;
using DeanOfficeApp.Api.Models;
using DeanOfficeApp.Contracts.Addresses;
using Microsoft.AspNet.Identity;
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
            initStudentMock();
            initEnrollmentMock();
            initLectureMock();
        }
        private Mock<IStudentRepository> studentMock = new Mock<IStudentRepository>();
        private Mock<IUserAddressRepository> adressMock = new Mock<IUserAddressRepository>();
        private Mock<IUserManager> userMock = new Mock<IUserManager>();
        private Mock<ILectureRepository> lectureMock = new Mock<ILectureRepository>();
        private Mock<ILoggingRepository> loggingMock = new Mock<ILoggingRepository>();
        private Mock<IEnrollmentRepository> enrollmentRepository = new Mock<IEnrollmentRepository>();
        private Mock<ILectureRepository> lectureRepository = new Mock<ILectureRepository>();
        private void initStudentMock()
        {
            Student[] students = new Student[]
            {
                new Student{ UserId = 1, RecordBookNumber = 271102, Pesel = 95062500222, CurrentSemester = 1, EnrollmentDate = DateTime.Now},
                new Student{ UserId = 2, RecordBookNumber = 271103, Pesel = 95062522222, CurrentSemester = 2, EnrollmentDate = DateTime.Now},
                new Student{ UserId = 3, RecordBookNumber = 271104, Pesel = 95062650222, CurrentSemester = 3, EnrollmentDate = DateTime.Now}
            };

            studentMock.Setup(m => m.GetStudents()).Returns(students);
            studentMock.Setup(m => m.GetStudentByUserId(1)).Returns(students[0]);
            studentMock.Setup(m => m.GetStudentByUserId(3)).Returns(students[2]);

        }
        private void initEnrollmentMock()
        {
            enrollments = new Enrollment[]
            {
                new Enrollment { StudentId = 1, LectureId = 1, EnrollmentDate = DateTime.Now },
                new Enrollment { StudentId = 2, LectureId = 3, EnrollmentDate = DateTime.Now },
                new Enrollment { StudentId = 3, LectureId = 2, EnrollmentDate = DateTime.Now }
            };
            enrollmentRepository.Setup(e => e.GetEnrollments(1)).Returns(enrollments);
            enrollmentRepository.Setup(e => e.InsertEnrollment(new Enrollment()));
            grades = new List<Grade>
            {
                new Grade{ EnrollementId = 1, GradeValue = new GradeValue(2.0), Date = DateTime.Now},
                new Grade{ EnrollementId = 1, GradeValue = new GradeValue(3.0), Date = DateTime.Now},
                new Grade{ EnrollementId = 1, Comment = "Komentarz do oceny", GradeValueId=null, Date=DateTime.Now},
                new Grade{ EnrollementId = 1, GradeValue = new GradeValue(4.0), Date = DateTime.Now},
            };
            enrollmentRepository.Setup(e => e.InsertGrade(grades[2])).Callback(() => grades.Add(grades[2]));

        }
        private void initLectureMock()
        {
            lectures = new Lecture[]
            {
                new Lecture{ Name ="Wykład1", EcstsPoints = 1, MinimalSemester = 3 },
                new Lecture{ Name ="Wykład2", EcstsPoints = 1, MinimalSemester = 3 },
                new Lecture{ Name ="Wykład3", EcstsPoints = 4, MinimalSemester = 1 }
            };
            lectureMock.Setup(m => m.GetLectureByID(1)).Returns(lectures[0]);

        }
        private Enrollment[] enrollments;
        private List<Grade> grades;
        private Lecture[] lectures;

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
        public static Mock<IUserManager> getUserMock()
        {
            if (_instance == null)
                _instance = new MockCreator();
            return _instance.getMockUser();
        }
        public static Mock<ILectureRepository> getLectureMock()
        {
            if (_instance == null)
                _instance = new MockCreator();
            return _instance.getMockLecture();
        }
        public static Mock<IEnrollmentRepository> getEnrollmentMock()
        {
            if (_instance == null)
                _instance = new MockCreator();
            return _instance.getMockEnrollment();
        }
        public static Mock<IUserAddressRepository> getAdressMock()
        {
            if (_instance == null)
                _instance = new MockCreator();
            return _instance.getMockAdress();
        }
        private Mock<IStudentRepository> getMockStudent()
        {
            return studentMock;
        }
        private Mock<IEnrollmentRepository> getMockEnrollment()
        {
            return enrollmentRepository;
        }
        private Mock<ILoggingRepository> getMockLogging()
        {
            return loggingMock;
        }
        private Mock<IUserManager> getMockUser()
        {
            return userMock;
        }
        private Mock<ILectureRepository> getMockLecture()
        {
            return lectureMock;
        }
        private Mock<IUserAddressRepository> getMockAdress()
        {
            return adressMock;
        }
    }
}
