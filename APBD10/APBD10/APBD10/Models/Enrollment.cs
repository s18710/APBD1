using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD10.Models
{
    public class Enrollment

    {
        public int idENrollment { get; set; }

        public int semsester { get; set; }

        public int idStudy { get; set; }

        public String startDate { get; set; }

        public Enrollment( int id, int sem, int idStud, String date)
        {
            idENrollment = id;
            semsester = sem;
            idStudy = idStud;
            startDate = date;
        }
    }
}
