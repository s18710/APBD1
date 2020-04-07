using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbdCW3.DAL;
using apbdCW3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using apbdCW3.Services;

namespace apbdCW3.Controllers
{   
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly SqlServerDbService dbService;

        public StudentsController(SqlServerDbService db)
        {
            dbService = db;
        }

        
        [HttpGet("student/{id}")]
        public IActionResult getStudent(int id)
        {
            return Ok(dbService.getStudent(id));
        }

        [HttpGet("getEnrollment/{id}")]
        public IActionResult getEnrollment(int id)
        {
            return Ok(dbService.getEnrollment(id));
        }

        [HttpGet]
        public IActionResult getStudents(string orderBy)
        {
            ArrayList array = dbService.getStudents(orderBy);
            if (array != null)
            {
                return Ok(array);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult addStudent(Student student)
        {
            Boolean b = dbService.addStudent(student);
            if (b)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult updateStudent(int id)
        {
            return Ok("Aktualizacja zakończona");
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            Boolean b = dbService.deleteStudent(id);
            if (b)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}