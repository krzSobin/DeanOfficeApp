using DeanOfficeApp.Contracts.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Enrollments
{
    public class GetEnrollmentDTO
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public int StudentRecordBookNumber { get; set; }
        public string LectureName { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public IEnumerable<GetGradeDTO> Grades { get; set; }
    }
}
