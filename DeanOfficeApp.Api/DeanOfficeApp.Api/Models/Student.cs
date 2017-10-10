using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeanOfficeApp.Api.Models
{
    public class Student
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.None)]
        public int StudentID { get; set; }

        //public ApplicationUser User { get; set; }
        public int RecordBookNumber { get; set; }
        public long Pesel { get; set; }
        public int CurrentSemester { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
