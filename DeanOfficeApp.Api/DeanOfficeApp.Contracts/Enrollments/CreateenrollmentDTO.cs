using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Enrollments
{
    public class CreateEnrollmentDTO
    {
        public int LectureId { get; set; }
        public int? UserId { get; set; }
    }
}
