using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Grades
{
    public class AddGradeResultDTO
    {
        public bool Added { get; set; }
        public GetGradeDTO Grade { get; set; }
    }
}
