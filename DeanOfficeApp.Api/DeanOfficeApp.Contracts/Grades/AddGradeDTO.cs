using System;

namespace DeanOfficeApp.Contracts.Grades
{
    public class AddGradeDTO
    {
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int? GradeValueId { get; set; }
        public int EnrollementId { get; set; }
    }
}
