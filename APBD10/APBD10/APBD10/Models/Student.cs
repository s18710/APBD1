using System;
using System.Collections.Generic;
using System.Text;

namespace Wyklad10Sample.Models
{
    public class Student
    {
        public int indexNumber { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String BirthDate { get; set; }

        public int enrollmentId { get; set; }

        public String studPassword { get; set; }

        public Student(int id, String fn, String ln, String bdate, int eid)
        {
            indexNumber = id;
            firstName = fn;
            lastName = ln;
            BirthDate = bdate;
            enrollmentId = eid;
            studPassword = null;
        }

    }

}
