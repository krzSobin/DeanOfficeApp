namespace DeanOfficeApp.Contracts.Teachers
{
    public class GetTeacherDTO
    {
        public int TeacherId { get; set; }
        public string Degree { get; set; } //todo wrzucić do słownika/enuma
        public string Position { get; set; }
        public string Room { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long Pesel { get; set; }
    }
}
