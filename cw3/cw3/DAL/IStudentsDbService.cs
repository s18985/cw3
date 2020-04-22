﻿using cw3.DTOs.Requests;
using cw3.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL
{
    public interface IStudentsDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);

        PromoteStudentsResponse PromoteStudents(PromoteStudentsRequest request);


    }
}
