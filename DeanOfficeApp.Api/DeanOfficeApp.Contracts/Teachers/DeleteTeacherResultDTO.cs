﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOfficeApp.Contracts.Teachers
{
    public class DeleteTeacherResultDTO
    {
        public bool DataDeleted { get; set; }
        public bool AccountDeleted { get; set; }
        public GetTeacherDTO Teacher { get; set; }
    }
}
