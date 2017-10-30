using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts
{
    public class CreateTeacherDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long Pesel { get; set; }
        public string Degree { get; set; } //todo wrzucić do słownika/enuma
        public string Position { get; set; }
        public string Room { get; set; }
    }
}
