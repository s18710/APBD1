using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbdCW3.Models;

namespace apbdCW3.Services
{
    public interface IStudentsDbService
    {
        public Enrollment promoteStudents(String Studies, String Semester);

        public Enrollment enrollStudent(Student s);

        public Student getStudent(int id);

        public Enrollment getEnrollment(int id);

        public ArrayList getStudents(string orderBy);

        public Boolean addStudent(Student student);

        public Boolean deleteStudent(int id);

        public Boolean checkStudentId(String id);
    }
}
