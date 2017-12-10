using DeanOfficeApp.Api.Models;
using System;
using System.Collections.Generic;

namespace DeanOfficeApp.Api.DAL.Teachers
{
    public interface ITeacherRepository : IDisposable
    {
        IEnumerable<Teacher> GetTeachers();
        Teacher GetTeacherByID(int teacherId);
        Teacher InsertTeacher(Teacher teacher);
        void DeleteTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        bool TeacherExists(int teacherId);
        bool Save();
    }
}