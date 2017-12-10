using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DeanOfficeApp.Api.Models
{
    public class Address
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.Identity)]
        public int Id { get; set; }
        public string City { get; set; }
        public string Road { get; set; }
        public string House { get; set; }
        public string Country { get; set; }
    }
}