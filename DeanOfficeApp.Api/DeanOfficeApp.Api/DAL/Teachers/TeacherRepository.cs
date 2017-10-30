using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeanOfficeApp.Api.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DeanOfficeApp.Api.DAL.Teachers
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext context;

        public TeacherRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteTeacher(Teacher teacher)
        {
            context.Teachers.Remove(teacher);
        }

        public Teacher GetTeacherByID(int teacherId)
        {
            return context.Teachers.Find(teacherId);
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return context.Teachers.ToList();
        }

        public Teacher InsertTeacher(Teacher teacher)
        {
            return context.Teachers.Add(teacher);
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

        public bool TeacherExists(int teacherId)
        {
            return context.Teachers.Count(e => e.TeacherId == teacherId) > 0;
        }

        public void UpdateTeacher(Teacher teacher)
        {
            context.Entry(teacher).State = EntityState.Modified;
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