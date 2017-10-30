using DeanOfficeApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeanOfficeApp.Api.DAL
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentByID(int studentId);
        Student InsertStudent(Student student);
        void DeleteStudent(Student student);
        void UpdateStudent(Student student);
        bool StudentExists(int studentId);
        bool Save();
    }
}