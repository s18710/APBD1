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

namespace apbdCW3.Controllers
{   
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

               public StudentsController()
        {
        }

        
        [HttpGet("student/{id}")]
        public IActionResult getStudent(int id)
        {
            Student st = new Student();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select * from Student where IndexNumber=@id";
                com.Parameters.AddWithValue("id", id);

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.indexNumber = dr["IndexNumber"].ToString();
                    st.idEnrollment = dr["IdEnrollment"].ToString();
                    st.birthDate = dr["BirthDate"].ToString();
                }

            }
            return Ok(st);
        }

        [HttpGet("enrollment/{id}")]
        public IActionResult getEnrollment(int id)
        {
            Enrollment e = new Enrollment();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select e.IdEnrollment, e.Semester, e.IdStudy, e.StartDate " +
                    " from Enrollment e, Student s where IndexNumber=@id and " +
                    "e.IdEnrollment=s.IdEnrollment";
                
                com.Parameters.AddWithValue("id", id);

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    e.IdEnrolllment = Int32.Parse( dr["IdEnrollment"].ToString());
                    e.idStudy = Int32.Parse(dr["IdStudy"].ToString());
                    e.semester = Int32.Parse(dr["Semester"].ToString());
                    e.startDater = dr["StartDate"].ToString();
                }

            }
            return Ok(e);
        }

        [HttpGet]
        public IActionResult getStudents(string orderBy)
        {
            ArrayList students = new ArrayList();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using(var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select * from Student";

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.indexNumber = dr["IndexNumber"].ToString();
                    st.idEnrollment = dr["IdEnrollment"].ToString();
                    st.birthDate = dr["BirthDate"].ToString();
                    students.Add(st);
                }

            }
            return Ok(students);
        }

        [HttpPost]
        public IActionResult addStudent(Student student)
        {
            int x;
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                if (client.State == ConnectionState.Closed)
                {
                    client.Open();
                }
                com.Connection = client;
                com.CommandText = "insert into Student values (@index, @fstN, @lstN, @date, @enrl )";
                com.Parameters.AddWithValue("index", student.indexNumber);
                com.Parameters.AddWithValue("fstN", student.FirstName);
                com.Parameters.AddWithValue("lstN", student.LastName);
                com.Parameters.AddWithValue("date", student.birthDate);
                com.Parameters.AddWithValue("enrl", student.idEnrollment);

                x = com.ExecuteNonQuery();

            }

            if (x == 0)
            {
                return BadRequest("command not executed properly");
            }
            else
            {
                return Ok("success");
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
            int x;
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                if (client.State == ConnectionState.Closed)
                {
                    client.Open();
                }
                com.Connection = client;
                com.CommandText = "delete from Student where IndexNumber = @id";
                com.Parameters.AddWithValue("id", id);
                x = com.ExecuteNonQuery();

            }

            if (x == 0)
            {
                return BadRequest("command not executed properly");
            }
            else
            {
                return Ok("success");
            }
        }
    }
}