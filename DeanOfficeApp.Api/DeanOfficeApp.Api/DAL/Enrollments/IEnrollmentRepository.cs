using DeanOfficeApp.Api.Models;
using System;
using System.Collections.Generic;

namespace DeanOfficeApp.Api.DAL.Enrollments
{
    public interface IEnrollmentRepository : IDisposable
    {
        Enrollment InsertEnrollment(Enrollment enrollment);
        IEnumerable<Enrollment> GetEnrollments(int userId);
        Enrollment GetEnrollmentById(int id);
        GradeValue GetGradeValueById(int gradeValueId);
        Grade InsertGrade(Grade grade);
        IEnumerable<Enrollment> GetEnrollmentsOfLecture(int lectureId);
        void DeleteEnrollment(Enrollment enrollment);
        bool Save();
        IEnumerable<GradeValue> GetGradeValues(string connectionString);
    }
}
