﻿using cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Services
{
    public interface IDBService
    {
        public IEnumerable<Student> GetStudents();
    }
}
