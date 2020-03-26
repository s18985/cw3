using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IDBService _dBService;

        public StudentsController(IDBService service)
        {
            _dBService = service;
        }

        //2.QueryString
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            //var s = HttpContext.Request; <---surowe dane z zadnia http
            //return $"Kowalski, Nowak, Malewski sortowanie={orderBy}"; <--- dla zwracanego stringa

            return Ok(_dBService.GetStudents());
        }

        //1.URL segment
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Nowak");
            }
            else
                return NotFound("Student not found");
        }

        //3. cialo zadania
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {

            // add to database
            // generating index number
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }


        [HttpPut("{id}")]
        public IActionResult ModifyStudent(int id)
        {
            return Ok("Aktualizacja dokonczona");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie zakonczone");
        }
    }
}