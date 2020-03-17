using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbdCW3.Models;

namespace apbdCW3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student> { 
                new Student { IdStudent=1, FirstName="John",LastName="Doe"},
                new Student { IdStudent=2, FirstName="Rick",LastName="Johnson"},
                new Student { IdStudent=3, FirstName="Anne",LastName="McCall"}};
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public Student getStudent(int id)
        {
            Student returnStudent = null;
            foreach(Student s in _students.ToList())
            {
                if (s.IdStudent == id)
                    returnStudent = s;
            }


            return returnStudent;
        }

        public bool addStudent(Student s)
        {
            if (s.FirstName.Equals("") || s.LastName.Equals("") || s.indexNumber.Equals(""))
            {
                return false;
            }
            else
            {   
                _students.ToList().Add(s);
                return true;
            }
        }

        public bool deleteStudent(int id)
        {
            Student stud = null;
            foreach (Student s in _students.ToList())
            {
                if (s.IdStudent == id)
                    stud = s;
            }
            if (stud != null) {
                _students.ToList().Remove(stud);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
