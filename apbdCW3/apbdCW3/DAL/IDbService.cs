using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbdCW3.Models;

namespace apbdCW3.DAL
{
    interface IDbService
    {
        public IEnumerable<Student> GetStudents();

        public Student getStudent(int id);

        public Boolean addStudent(Student s);

        public Boolean deleteStudent(int id);
    }
}
