using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Students
{
    public class DeleteStudentResultDTO
    {
        public bool DataDeleted { get; set; }
        public bool AccountDeleted { get; set; }
        public GetStudentDTO Student { get; set; }
    }
}
