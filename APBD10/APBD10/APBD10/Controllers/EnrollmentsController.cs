using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wyklad10Sample.Models;

namespace APBD10.Controllers
{
    public class EnrollmentsController : ControllerBase
    {
        private readonly StudentsDbContext _context;
        public EnrollmentsController(StudentsDbContext context)
        {
            _context = context;
        }

        public IActionResult EnrollStudent(String fName, String lName, int index, String bdate, String studies)
        {
            if(fName == null || lName == null || index == null || bdate == null || studies == null)
            {
                return BadRequest();
            }
            else
            {
                bool studiesexists = _context.Studies.Any(s => s.name == studies);
                if (studiesexists)
                {
                    var idStudies = _context.Studies.Where(s => s.name == studies).Select(s => new
                    {
                        id = s.idStudy
                    }).SingleOrDefault();
                    bool enrollmentExists = _context.Enrollments.Any(e => (e.idStudy == idStudies.id) && (e.semsester == 1) );

                    int enrlId = 0;
                    if (!enrollmentExists)
                    {
                        int maxId = _context.Enrollments.Max(u => u.idENrollment);
                        Enrollment enrollment = new Enrollment(maxId+1, 1, idStudies.id , DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        _context.Enrollments.Add(enrollment);
                        _context.SaveChanges();
                        enrlId = maxId + 1;
                    }
                    else
                    {
                        var tmp = _context.Enrollments.Where(e => (e.idStudy == idStudies.id) && (e.semsester == 1)).Select(s => new
                        {
                            id = s.idENrollment
                        }).FirstOrDefault();

                        enrlId = tmp.id;
                    }

                    Student stud = new Student(index, fName, lName, bdate, enrlId);
                    _context.Students.Add(stud);
                    var enrollmentFin = _context.Enrollments.Where(e => e.idENrollment == enrlId).FirstOrDefault();
                    return CreatedAtAction("student enrolled",enrollmentFin);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        public IActionResult PromoteStudents(String studies, int sem)
        {
            if (studies == null || sem == null)
            {
                return BadRequest();
            }
            else
            {
                var idStudies = _context.Studies.Where(s => s.name == studies).Select(s => new
                {
                    id = s.idStudy
                }).SingleOrDefault();
                bool enrollmentExists = _context.Enrollments.Any(e => (e.idStudy == idStudies.id) && (e.semsester == sem));

                if (!enrollmentExists)
                {
                    return BadRequest();
                }
                else
                {
                    var tmp = _context.Enrollments.Where(e => (e.idStudy == idStudies.id) && (e.semsester == sem)).Select(s => new
                    {
                        id = s.idENrollment
                    }).FirstOrDefault();

                    var oldenrlId = tmp.id;


                    var nenrlId = 0;
                    enrollmentExists = _context.Enrollments.Any(e => (e.idStudy == idStudies.id) && (e.semsester == sem+1));
                    if (!enrollmentExists)
                    {
                        int maxId = _context.Enrollments.Max(u => u.idENrollment);
                        Enrollment nenr = new Enrollment(maxId + 1, sem+1, idStudies.id, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        _context.Enrollments.Add(nenr);
                        _context.SaveChanges();
                        nenrlId = maxId + 1;
                    }
                    else
                    {
                        tmp = _context.Enrollments.Where(e => (e.idStudy == idStudies.id) && (e.semsester == sem+1)).Select(s => new
                        {
                            id = s.idENrollment
                        }).FirstOrDefault();

                        nenrlId = tmp.id;
                    }

                    var students = _context.Students.Where(s => s.enrollmentId == oldenrlId);

                    students.ForEachAsync(s => s.enrollmentId = nenrlId);
                    _context.SaveChanges();

                    var enrollmentFin = _context.Enrollments.Where(e => e.idENrollment == nenrlId).FirstOrDefault();
                    return CreatedAtAction("student enrolled", enrollmentFin);
                }
            }
        }
    }
}