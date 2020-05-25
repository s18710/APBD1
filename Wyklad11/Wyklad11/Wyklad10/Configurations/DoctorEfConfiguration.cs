using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wyklad10.Models;

namespace Wyklad10.Configurations
{
    public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        var doctors = new List<Doctor>();
        doctors.Add(new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Dzban", Email = "jd@lekarz.pl" });
        doctors.Add(new Doctor { IdDoctor = 2, FirstName = "Emma", LastName = "Goldman", Email = "eg@lekarz.pl" });

        builder.HasData(doctors);
    }
}
}
