using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Teachers
{
    public class NewTeacherResultDTO
    {
        public bool Created { get; set; }
        public GetTeacherDTO Teacher { get; set; }
    }
}
