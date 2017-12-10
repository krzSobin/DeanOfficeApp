namespace DeanOfficeApp.Contracts.Teachers
{
    public class DeleteTeacherResultDTO
    {
        public bool DataDeleted { get; set; }
        public bool AccountDeleted { get; set; }
        public GetTeacherDTO Teacher { get; set; }
    }
}
