using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using apbdCW3.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbdCW3.Controllers
{
    [ApiController]
    [Route("api/enrollment")]
    public class EnrollmentController : ControllerBase
    {


        public EnrollmentController() { }

        [HttpPost]
        public IActionResult enrollStudent(Student s) { 
        
            if(s.FirstName.Equals("") || s.LastName.Equals("") || s.indexNumber.Equals("") || s.Studies.Equals(""))
            {
                return NotFound();
            }

            IActionResult iactionRes = null;
            Enrollment e = null;
            int x;
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
               
                com.Connection = client;
                com.CommandText = "select idStudy from Studies where name like @sName";
                com.Parameters.AddWithValue("sName", s.Studies);

                client.Open();
                var dr = com.ExecuteReader();
                var studiesExist = -1;
                while (dr.Read())
                {
                    studiesExist = Int32.Parse(dr["IdStudy"].ToString());
                }

                if (studiesExist == -1)
                {
                    return NotFound();
                }


                com.CommandText = "select e.idEnrollment from Enrollment e where Semester = 1 and idStudy like " + studiesExist;
                com.ExecuteNonQuery();

                client.Open();
                dr = com.ExecuteReader();
                var enrollmentId = -1;
                while (dr.Read())
                {
                    enrollmentId = Int32.Parse(dr["IdEnrollment"].ToString());
                }

                SqlTransaction transaction;
                transaction = client.BeginTransaction("EnrollmentTransaction");
                com.Transaction = transaction;
                try
                {
                    if (enrollmentId == -1 || enrollmentId == null)
                    {
                        com.CommandText = "select MAX(e.IdEnrollment) as mx from Enrollment e ";

                        client.Open();
                        dr = com.ExecuteReader();
                        enrollmentId = -1;
                        while (dr.Read())
                        {
                            enrollmentId = Int32.Parse(dr["mx"].ToString());
                        }

                        enrollmentId++;
                        com.CommandText = "insert into Enrollment values(@eId, @sem, @idStud, @dat )";
                        com.Parameters.AddWithValue("eId", enrollmentId);
                        com.Parameters.AddWithValue("sem", 1);
                        com.Parameters.AddWithValue("idStud", studiesExist);
                        com.Parameters.AddWithValue("dat", DateTime.Now);
                        com.ExecuteNonQuery();


                        com.CommandText = "select count(*) as cnt from Student where IndexNumber = @idx";
                        com.Parameters.AddWithValue("idx", s.indexNumber);
                        com.ExecuteNonQuery();

                        client.Open();
                        dr = com.ExecuteReader();
                        var unique = -1;
                        while (dr.Read())
                        {
                            unique = Int32.Parse(dr["cnt"].ToString());
                        }

                        if (unique > 1)
                        {
                            transaction.Rollback();
                            return BadRequest();
                        }

                        com.CommandText = "insert into Student values (@index, @fstN, @lstN, @date, @enrl )";
                        com.Parameters.AddWithValue("index", s.indexNumber);
                        com.Parameters.AddWithValue("fstN", s.FirstName);
                        com.Parameters.AddWithValue("lstN", s.LastName);
                        com.Parameters.AddWithValue("date", s.birthDate);
                        com.Parameters.AddWithValue("enrl", enrollmentId);

                        com.ExecuteNonQuery();
                        transaction.Commit();


                        com.CommandText = "select * from Enrollment where IdEnrollment =" + enrollmentId;
                        client.Open();
                        dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            e.IdEnrolllment = Int32.Parse(dr["IdEnrollment"].ToString());
                            e.idStudy = Int32.Parse(dr["IdStudy"].ToString());
                            e.semester = Int32.Parse(dr["Semester"].ToString());
                            e.startDater = dr["StartDate"].ToString();
                        }

                    }
                    else
                    {
                        return BadRequest();
                    }
                }catch(SqlException ex)
                {
                    transaction.Rollback();
                    return BadRequest();
                }

                return CreatedAtAction("enrolled student", e);
            }
        }

        [HttpPost("promotions")]

        public IActionResult promoteStudents(String Studies, String Semester)
        {
            Enrollment e = new Enrollment();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18710;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select count(*) as cnt from Enrollment e, Studies s where s.IdStudy = e.IdStudy " +
                    "and s.Name like @studies and e.Semester= @sem ";

                com.Parameters.AddWithValue("studies", Studies);
                com.Parameters.AddWithValue("sem", Semester);

                client.Open();
                var dr = com.ExecuteReader();
                int count = -1;
                while (dr.Read())
                {
                    count = Int32.Parse(dr["cnt"].ToString());
                }

                if(count != 1)
                {
                    return NotFound();
                }

                com.CommandText = "select count(*) as cnt from Enrollment e, Studies s where s.IdStudy = e.IdStudy " +
                    "and s.Name like @studies and e.Semester= @sem ";

                com.Parameters.AddWithValue("studies", Studies);
                com.Parameters.AddWithValue("sem", Int16.Parse(Semester)+1);

                client.Open();
                dr = com.ExecuteReader();
                count = -1;
                while (dr.Read())
                {
                    count = Int32.Parse(dr["cnt"].ToString());
                }

                if (count < 1)
                {
                    com.CommandText = "select max(IdEnrollment) as mx from Enrollment";
                    client.Open();
                    dr = com.ExecuteReader();
                    var id = -1;
                    while (dr.Read())
                    {
                        id = Int32.Parse(dr["mx"].ToString());
                    }

                    com.CommandText = "select IdStudy as stud from Studies where Name like @name";
                    com.Parameters.AddWithValue("name", Studies);
                    client.Open();
                    dr = com.ExecuteReader();
                    var studId = -1;
                    while (dr.Read())
                    {
                        studId = Int32.Parse(dr["mx"].ToString());
                    }

                    com.CommandText = "insert into Enrollment values(@eId, @sem, @idStud, @dat )";
                    com.Parameters.AddWithValue("eId", id+1);
                    com.Parameters.AddWithValue("sem", 2);
                    com.Parameters.AddWithValue("idStud", studId);
                    com.Parameters.AddWithValue("dat", DateTime.Now);
                    com.ExecuteNonQuery();

               
                }

                com.CommandText = "select max(IdEnrollment) as mx from Enrollment";
                client.Open();
                dr = com.ExecuteReader();
                var maxid = -1;
                while (dr.Read())
                {
                    maxid = Int32.Parse(dr["mx"].ToString());
                }

                com.Connection = client;
                com.CommandText = "select e.IdEnrollment from Enrollment e, Studies s where s.IdStudy = e.IdStudy " +
                    "and s.Name like @studies and e.Semester= @sem ";

                com.Parameters.AddWithValue("studies", Studies);
                com.Parameters.AddWithValue("sem", Semester);

                client.Open();
                dr = com.ExecuteReader();
                int oldId = -1;
                while (dr.Read())
                {
                    oldId = Int32.Parse(dr["IdEnrollment"].ToString());
                }

                com.CommandText = "update Student set IdEnrollment=@newId where IdEnrollment=@oldId";
                com.Parameters.AddWithValue("newId", maxid + 1);
                com.Parameters.AddWithValue("oldId", oldId);


                com.Connection = client;
                com.CommandText = "select * from Enrollment e where " +
                    "EnrollmentId = ";
                com.Parameters.AddWithValue("newId", maxid + 1);
               

                client.Open();
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    e.IdEnrolllment = Int32.Parse(dr["IdEnrollment"].ToString());
                    e.idStudy = Int32.Parse(dr["IdStudy"].ToString());
                    e.startDater = dr["StartDate"].ToString();
                    e.semester = Int32.Parse(dr["Semester"].ToString());
                }
            }
            return CreatedAtAction("Students Promoted", e);
        }
    }
}