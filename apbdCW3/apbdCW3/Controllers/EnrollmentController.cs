using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using apbdCW3.Models;
using apbdCW3.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbdCW3.Controllers
{
    [ApiController]
    [Route("api/enrollment")]
    public class EnrollmentController : ControllerBase
    {
        private readonly SqlServerDbService dbService;

        public EnrollmentController(SqlServerDbService db)
        {
            dbService = db;
        }

        [HttpPost]
        public IActionResult enrollStudent(Student s)
        {

            Enrollment e = dbService.enrollStudent(s);
            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("promotions")]

        public IActionResult promoteStudents(String Studies, String Semester)
        {

            Enrollment e = dbService.promoteStudents(Studies, Semester);
            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}