using System;

namespace DeanOfficeApp.Contracts.Grades
{
    public class GetGradeDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public double? GradeValue { get; set; }
    }
}
