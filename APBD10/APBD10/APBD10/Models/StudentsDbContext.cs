using System;
using System.Collections.Generic;
using System.Text;
using APBD10.Models;
using Microsoft.EntityFrameworkCore;

namespace Wyklad10Sample.Models
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Studies> Studies { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public StudentsDbContext() { }

        public StudentsDbContext(DbContextOptions options) : base(options){
            
        }
    }
}
