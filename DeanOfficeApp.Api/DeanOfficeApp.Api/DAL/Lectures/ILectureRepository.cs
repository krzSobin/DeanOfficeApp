using DeanOfficeApp.Api.Models;
using System;
using System.Collections.Generic;

namespace DeanOfficeApp.Api.DAL.Lectures
{
    public interface ILectureRepository : IDisposable
    {
        IEnumerable<Lecture> GetLectures();
        Lecture GetLectureByID(int lectureId);
        Lecture InsertLecture(Lecture lecture);
        void DeleteLecture(Lecture lecture);
        void UpdateLecture(Lecture lecture);
        bool LectureExists(int lectureId);
        bool Save();
        IEnumerable<Lecture> GetLecturesAvailableForEnroll(int userId, int minimalSemester);
        IEnumerable<Lecture> GetLecturesOfTeacher(int userId);
    }
}
