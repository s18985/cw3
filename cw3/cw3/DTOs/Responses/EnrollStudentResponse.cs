﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DTOs.Responses
{
    public class EnrollStudentResponse
    {

        public string IndexNumber { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }

    }
}
