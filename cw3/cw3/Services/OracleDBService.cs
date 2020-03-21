using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw3.Services
{
    public class OracleDbService : IDbService
    {
        public IEnumerable<Student> GetStudents()
        {
            //real db connection
            throw new NotImplementedException();
        }
    }
}
