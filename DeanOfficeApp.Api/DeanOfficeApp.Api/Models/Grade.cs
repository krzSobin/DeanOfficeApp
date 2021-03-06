﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeanOfficeApp.Api.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int? GradeValueId { get; set; }
        public virtual GradeValue GradeValue { get; set; }

        public int EnrollementId { get; set; }
        public virtual Enrollment Enrollement { get; set; }
    }

    public class GradeValue
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int Id { get; set; }

        [Range(2.0, 5.0)]
        public double Value { get; set; }
    }
}