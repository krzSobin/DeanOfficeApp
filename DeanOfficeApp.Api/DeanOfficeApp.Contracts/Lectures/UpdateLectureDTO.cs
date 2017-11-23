﻿namespace DeanOfficeApp.Contracts.Lectures
{
    public class UpdateLectureDTO
    {
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Bibliography { get; set; }
        public int MinimalSemester { get; set; }
        public int EcstsPoints { get; set; }
        public int TeacherId { get; set; }
    }
}
