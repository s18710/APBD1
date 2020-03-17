using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbdCW3.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbdCW3.Controllers
{   
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
      
        
        [HttpGet("{id}")]
        public IActionResult getStudent(int id)
        {
            if(id == 1)
            {
                return Ok("Kowalski");
            }else if (id == 2)
            {
                return Ok("Majewska");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpGet]
        public String getStudents(string orderBy)
        {
            return $"Kowalski, Majewska, Panko sortowanie={orderBy}";
        }

        [HttpPost]
        public IActionResult addStudent(Student student)
        {
            student.indexNumber = $"s{new Random().Next(1,20000)}";
            return Ok(student);
        }
    }
}