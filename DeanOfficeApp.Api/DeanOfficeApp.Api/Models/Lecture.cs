using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeanOfficeApp.Api.Models
{
    public class Lecture
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Bibliography { get; set; }
        public int MinimalSemester { get; set; }
        public int EcstsPoints { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}