using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeanOfficeApp.Api.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int TeacherId { get; set; }
        public string Degree { get; set; } //todo wrzucić do słownika/enuma
        public string Position { get; set; }
        public string Room { get; set; }

        [Index(IsUnique = true)]
        public long Pesel { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser UserData { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}