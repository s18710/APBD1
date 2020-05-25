using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wyklad10Sample.Models;

namespace APBD10.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly StudentsDbContext _context;
        public StudentController(StudentsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getStudents()
        {
            return Ok(_context.Students.ToList());
        }

        [HttpPost]
        public IActionResult updateStudent(Student student)
        {
            if (student.indexNumber != null) {

                bool exists = _context.Students.Any(s => s.indexNumber == student.indexNumber);
                if(exists)
                {
                    _context.Update(student);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]

        public IActionResult deletStudent(int id)
        {
            if (id != null)
            {

                bool exists = _context.Students.Any(s => s.indexNumber == id);
                if (exists)
                {
                    var student = _context.Students.First(s => s.indexNumber == id);
                    _context.Remove(student);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}