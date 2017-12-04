using DeanOfficeApp.Contracts.Addresses;
using System;

namespace DeanOfficeApp.Contracts
{
    public class CreateStudentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long Pesel { get; set; }
        public int CurrentSemester { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public AddAddressDTO Address { get; set; }
    }
}
