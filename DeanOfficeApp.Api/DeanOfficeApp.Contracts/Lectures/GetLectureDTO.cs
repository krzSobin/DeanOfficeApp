namespace DeanOfficeApp.Contracts.Lectures
{
    public class GetLectureDTO
    {
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Bibliography { get; set; }
        public int MinimalSemester { get; set; }
        public int EcstsPoints { get; set; }
        public string Teacher { get; set; }
    }
}
