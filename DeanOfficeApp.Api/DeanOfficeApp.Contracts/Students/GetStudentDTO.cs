using System;
using System.Collections.Generic;

namespace DeanOfficeApp.Contracts.Students
{
    public class GetStudentDTO
    {
        public int RecordBookNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long Pesel { get; set; }
        public int CurrentSemester { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
