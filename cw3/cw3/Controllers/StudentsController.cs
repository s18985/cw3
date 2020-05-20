using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using cw3.DAL;
using cw3.DTOs.Requests;
using cw3.Models;
using cw3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cw3.Controllers
{
    
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        //private IDBService _dBService;
        private IStudentsDbService _SqlService;
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18985;Integrated Security=True";

        public StudentsController(IConfiguration configuration, IStudentsDbService SqlService)
        {
            //_dBService = DbService;
            Configuration = configuration;
            _SqlService = SqlService;
        }

        //2.QueryString
        [HttpGet]
        public IActionResult GetStudents()//string orderBy)
        {

            var list = new List<Student>();

            using(var con = new SqlConnection(ConString))
            using(var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student;";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.Birthdate = (DateTime) dr["BirthDate"];
                    list.Add(st);
                }

                return Ok(list);

            }

            //var s = HttpContext.Request; <---surowe dane z zadnia http
            //return $"Kowalski, Nowak, Malewski sortowanie={orderBy}"; <--- dla zwracanego stringa

            //return Ok(_dBService.GetStudents());
        }


        [HttpGet("{indexNumber}")]
        public IActionResult GetEnrollment(string indexNumber)
        {

            var list2 = new List<Enrollment>();

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Enrollment inner join Student on Enrollment.IdEnrollment = Student.IdEnrollment where Student.IndexNumber = @index";
                com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {

                    var en = new Enrollment();
                    en.IndexNumber = dr["IndexNumber"].ToString();
                    en.IdEnrollment = (int) dr["IdEnrollment"];
                    en.Semester = (int) dr["Semester"];
                    en.IdStudy = (int)dr["IdStudy"];
                    en.StartDate = dr["StartDate"].ToString();
                    list2.Add(en);
                }

                return Ok(list2);

            }
        }

        /*
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
        */

        //3. cialo zadania
        /*
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {

            // add to database
            // generating index number
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        */
        

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

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var response = _SqlService.Login(request);

                if (response != null)
                {
                    var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, response.Login),
                new Claim(ClaimTypes.Role, "student")
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken
                    (
                        //issuer: "Gakko",
                        //audience: "Students",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = Guid.NewGuid()
                    });

                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception exc)
            {
                return Unauthorized();
            }

            
        }
    }
}