using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wyklad10.Models;

namespace Wyklad10.Configurations
{
    public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            var prescriptionDictionary = new List<Prescription>();
            prescriptionDictionary.Add(new Prescription { IdPrescription = 1, IdPatient = 1, IdDoctor = 1, Date = DateTime.Now, DueDate = new DateTime(2020, 7, 4) });
            prescriptionDictionary.Add(new Prescription { IdPrescription = 2, IdPatient = 1, IdDoctor = 1, Date = DateTime.Now, DueDate = new DateTime(2020, 7, 12) });
            prescriptionDictionary.Add(new Prescription { IdPrescription = 3, IdPatient = 2, IdDoctor = 2, Date = DateTime.Now, DueDate = new DateTime(2020, 8, 4) });
        }
    }
}
