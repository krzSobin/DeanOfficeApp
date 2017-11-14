namespace DeanOfficeApp.Contracts.Enrollments
{
    public class DeleteEnrollmentResultDTO
    {
        public bool Deleted { get; set; }
        public GetEnrollmentDTO Enrollment { get; set; }
    }
}
