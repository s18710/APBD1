using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wyklad10.Models;

namespace Wyklad10.Services
{
    public interface IDoctorsDbService
    {
        public IEnumerable<Doctor> GetDoctors();

        public Boolean InsertDoctor(Doctor doctor);

        public Boolean UpdateDoctor(Doctor doctor);

        public Boolean DeletetDoctor(int drId);
    }
}
