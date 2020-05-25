using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wyklad10.Models;

namespace Wyklad10.Services
{
    public class DoctorsService : IDoctorsDbService
    {
        private readonly HospitalDbContext _context;
        public DoctorsService(HospitalDbContext context)
        {
            _context = context;
        }

        public bool DeletetDoctor(int drId)
        {
            var delete = _context.Doctors.FirstOrDefault(d => d.IdDoctor == drId);
            if(delete != null)
            {
                _context.Doctors.Remove(delete);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }

        public bool InsertDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            return true;
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            var doctorUpdate = _context.Doctors.FirstOrDefault(d => d.IdDoctor == doctor.IdDoctor);
            if(doctorUpdate != null)
            {
                doctorUpdate.FirstName = doctor.FirstName;
                doctorUpdate.LastName = doctor.LastName;
                doctorUpdate.Email = doctor.Email;

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
