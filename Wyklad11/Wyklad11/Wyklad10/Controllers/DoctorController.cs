using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wyklad10.Models;
using Wyklad10.Services;

namespace Wyklad10.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorsDbService _context;

        public DoctorController(IDoctorsDbService context)
        {
            _context = context;
        }

        //CRUD api put - update post- insert

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_context.GetDoctors());
        }

        [HttpDelete]
        public IActionResult DeleteDoctor(int id)
        {
            return Ok(_context.DeletetDoctor(id));
        }

        [HttpPut]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            return Ok(_context.UpdateDoctor(doctor));
        }

        [HttpPost]
        public IActionResult InsertDoctor(Doctor doctor)
        {
            return Ok(_context.InsertDoctor(doctor));
        }

    }
}