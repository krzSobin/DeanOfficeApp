namespace DeanOfficeApp.Contracts.Students
{
    public class DeleteStudentResultDTO
    {
        public bool DataDeleted { get; set; }
        public bool AccountDeleted { get; set; }
        public GetStudentDTO Student { get; set; }
    }
}
