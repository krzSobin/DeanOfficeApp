using DeanOfficeApp.Api.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DeanOfficeApp.Api.DAL.Lectures
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext context;

        public LectureRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteLecture(Lecture lecture)
        {
            context.Lectures.Remove(lecture);
        }

        public Lecture GetLectureByID(int lectureId)
        {
            return context.Lectures.Find(lectureId);
        }

        public IEnumerable<Lecture> GetLectures()
        {
            return context.Lectures.ToList();
        }

        public IEnumerable<Lecture> GetLecturesAvailableForEnroll(int userId, int currentSemester)
        {
            var enrollments = context.Enrollments.Where(e => e.Student.UserId == userId);

            return context.Lectures.Where(l => !l.Enrollments.Any(e => enrollments.Contains(e)) && l.MinimalSemester <= currentSemester).ToList();
        }

        public Lecture InsertLecture(Lecture lecture)
        {
            return context.Lectures.Add(lecture);
        }

        public bool Save()
        {
            try
            {
                return context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public bool LectureExists(int lectureId)
        {
            return context.Lectures.Count(e => e.LectureId == lectureId) > 0;
        }

        public void UpdateLecture(Lecture lecture)
        {
            context.Entry(lecture).State = EntityState.Modified;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}