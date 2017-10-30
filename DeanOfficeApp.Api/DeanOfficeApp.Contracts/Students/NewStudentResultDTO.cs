using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Students
{
    public class NewStudentResultDTO
    {
        public bool Created { get; set; }
        public GetStudentDTO Student { get; set; }
    }
}
