using cw3.Models;
using System.Collections.Generic;

namespace cw3.Services
{
    public class MockDBService : IDBService
    {
        private static IEnumerable<Student> _students = new List<Student>
        {
            new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski", IndexNumber="s1234"},
            new Student{IdStudent=2, FirstName="Andrzej", LastName="Malina", IndexNumber="s2345"},
            new Student{IdStudent=3, FirstName="Monika", LastName="Nowak", IndexNumber="s3456"}
        };

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
