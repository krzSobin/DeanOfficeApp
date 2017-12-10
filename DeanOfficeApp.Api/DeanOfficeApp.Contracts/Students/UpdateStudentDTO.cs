using System;

namespace DeanOfficeApp.Contracts.Students
{
    public class UpdateStudentDTO
    {
        public int RecordBookNumber { get; set; }
        public long Pesel { get; set; }
        public int CurrentSemester { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
