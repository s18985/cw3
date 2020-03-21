using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IDBService _dBService;

        public StudentsController(IDBService service)
        {
            _dBService = service;
        }

        //2.QueryString
        [HttpGet]
        public string GetStudents(string orderBy)
        {
            var s = HttpContext.Request;
            
            return "Jan, Anna, Krzysztof sortowanie={orderBy}";
        }

        //1.URL segment
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if(id == 1)
            {
                return Ok("Jan");
            }else
            return NotFound("Student not found");
        }

        //3. cialo zadania
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            //...
            return Ok(student);
        }
    }
}