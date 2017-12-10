using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeanOfficeApp.Api.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int LectureId { get; set; }
        public virtual Lecture Lecture { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}