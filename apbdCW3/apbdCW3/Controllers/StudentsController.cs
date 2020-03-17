using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbdCW3.DAL;
using apbdCW3.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbdCW3.Controllers
{   
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private readonly IDbService _dbService;

        private StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        
        [HttpGet("{id}")]
        public IActionResult getStudent(int id)
        {
            if(_dbService.getStudent(id) != null)
            {
                return Ok(_dbService.getStudent(id));
            }
            return NotFound("Nie znaleziono studenta");
        }

        [HttpGet]
        public IActionResult getStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpPost]
        public IActionResult addStudent(Student student)
        {
            student.indexNumber = $"s{new Random().Next(1,20000)}";
            _dbService.addStudent(student);
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult updateStudent(int id)
        {
            return Ok("Aktualizacja zakończona");
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            if (_dbService.deleteStudent(id))
            {
                return Ok("Usuwanie zakonczone");
            }
            else
            {
                return NotFound("student o podanym id nie istnieje");
            }
        }
    }
}