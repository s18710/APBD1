using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wyklad10.Models;

namespace Wyklad10.Configurations
{
    public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            var patients = new List<Patient>();
            patients.Add(new Patient { IdPatient = 1, FirstName = "Jim", LastName = "Jim", Birthdate = new DateTime(1999,3,4) });
            patients.Add(new Patient { IdPatient = 2, FirstName = "Anna", LastName = "Anarchist", Birthdate = new DateTime(1998, 7, 4) });

            builder.HasData(patients);
        }
    }
}
