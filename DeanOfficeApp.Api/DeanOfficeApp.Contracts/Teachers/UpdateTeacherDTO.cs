using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Teachers
{
    public class UpdateTeacherDTO
    {
        public int TeacherId { get; set; }
        public string Degree { get; set; } //todo wrzucić do słownika/enuma
        public string Position { get; set; }
        public string Room { get; set; }
        public long Pesel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
