using DeanOfficeApp.Api.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;

namespace DeanOfficeApp.Api.DAL
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Student> GetStudents(string connectionString)
        {
            var students = new List<Student>();
            var queryString = "SELECT s.RecordBookNumber, s.UserId, s.Pesel, s.CurrentSemester, s.Enrollmentdate, u.FirstName, u.LastName, u.Email FROM Students s JOIN AspNetUsers u ON s.UserId = u.Id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                RecordBookNumber = reader.GetInt32(0),
                                CurrentSemester = reader.GetInt32(3),
                                EnrollmentDate = reader.GetDateTime(4),
                                Pesel = reader.GetInt64(2),
                                UserId = reader.GetInt32(1),
                                UserData = new ApplicationUser
                                {
                                    FirstName = reader.GetString(5),
                                    LastName = reader.GetString(6),
                                    Email = reader.GetString(7)
                                }
                            });
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }

            return students;
        }

        public Student GetStudentByID(int id)
        {
            return context.Students.Find(id);
        }

        public Student GetStudentByUserId(int userId)
        {
            return context.Students.FirstOrDefault(s => s.UserId == userId);
        }

        public Student InsertStudent(Student student)
        {
            return context.Students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            context.Students.Remove(student);
        }

        public void UpdateStudent(Student student)
        {
            context.Entry(student).State = EntityState.Modified;
        }

        public bool StudentExists(int studentId)
        {
            return context.Students.Count(e => e.RecordBookNumber == studentId) > 0;
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

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}