using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DeanOfficeApp.Api.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.None)]
        public int TeacherId { get; set; }

        //public ApplicationUser User { get; set; }
        public string Degree { get; set; } //todo wrzucić do słownika/enuma
        public string Position { get; set; }
        public string Room { get; set; }
        public int EcstsPoints { get; set; }
    }
}