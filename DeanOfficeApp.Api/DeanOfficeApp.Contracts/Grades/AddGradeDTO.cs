using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
