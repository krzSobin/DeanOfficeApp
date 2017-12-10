namespace DeanOfficeApp.Contracts.Enrollments
{
    public class CreateEnrollmentResultDTO
    {
        public bool Created { get; set; }
        public string Error { get; set; }
        public GetEnrollmentDTO Enrollment { get; set; }

        public CreateEnrollmentResultDTO(bool created, string error, GetEnrollmentDTO enrollment)
        {
            Created = created;
            Error = error;
            Enrollment = enrollment;
        }
    }
}
