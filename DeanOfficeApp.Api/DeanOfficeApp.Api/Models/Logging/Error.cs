using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeanOfficeApp.Api.Models.Logging
{
    public class Error
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}