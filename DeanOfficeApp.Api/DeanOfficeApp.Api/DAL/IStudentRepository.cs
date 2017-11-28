using DeanOfficeApp.Api.Models;
using System;
using System.Collections.Generic;

namespace DeanOfficeApp.Api.DAL
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentByID(int studentId);
        Student GetStudentByUserId(int userId);
        Student InsertStudent(Student student);
        void DeleteStudent(Student student);
        void UpdateStudent(Student student);
        bool StudentExists(int studentId);
        bool Save();
    }
}