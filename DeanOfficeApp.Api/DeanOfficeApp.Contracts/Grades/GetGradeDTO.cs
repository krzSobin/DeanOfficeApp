using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
