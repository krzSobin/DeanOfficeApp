using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeanOfficeApp.Api.Models;
using System.Data.Entity.Validation;

namespace DeanOfficeApp.Api.DAL.Enrollments
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<Enrollment> GetEnrollments()
        {
            return context.Enrollments;
        }

        public IEnumerable<Enrollment> GetEnrollmentsOfLecture(int lectureId)
        {
            return context.Enrollments.Where(e => e.LectureId == lectureId).OrderBy(e => e.Student.UserData.LastName).ToList();
        }

        public Enrollment InsertEnrollment(Enrollment enrollment)
        {
            return context.Enrollments.Add(enrollment);
        }

        public void DeleteEnrollment(Enrollment enrollment)
        {
            context.Enrollments.Remove(enrollment);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EnrollmentRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}