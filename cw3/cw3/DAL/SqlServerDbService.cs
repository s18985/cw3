using cw3.DTOs.Requests;
using cw3.DTOs.Responses;
using cw3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.DAL
{
    public class SqlServerDbService : IStudentsDbService
    {
       
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var st = new Student();
            st.IndexNumber = request.IndexNumber;
            st.FirstName = request.FirstName;
            st.LastName = request.LastName;
            st.Birthdate = request.Birthdate;
            var response = new EnrollStudentResponse();
            int tmp;
            DateTime date = DateTime.Today;
            const string ConString = "Data Source=db-mssql;Initial Catalog=s18985;Integrated Security=True";

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();

                com.CommandText = "execute EnrollStudent @index, @fn, @ln, @birth, @stud";
                com.Parameters.AddWithValue("index", request.IndexNumber);
                com.Parameters.AddWithValue("fn", request.FirstName);
                com.Parameters.AddWithValue("ln", request.LastName);
                com.Parameters.AddWithValue("birth", request.Birthdate);
                com.Parameters.AddWithValue("stud", request.Studies);

                tmp = com.ExecuteNonQuery();

                response.IndexNumber = st.IndexNumber;
                response.Semester = 1;
                response.StartDate = date;

            }
            if (tmp > 0)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public void PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }
    }
}
