using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wyklad10.Models;

namespace Wyklad10.Configurations
{
    public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            var prescriptionMedicamentDisctionary = new List<PrescriptionMedicament>();
            prescriptionMedicamentDisctionary.Add(new PrescriptionMedicament { IdMedicament = 1, IdPrescription=1, Dose = 120, Details = "" });
            prescriptionMedicamentDisctionary.Add(new PrescriptionMedicament { IdMedicament = 4, IdPrescription = 2, Dose = 1, Details = "asap" });
            prescriptionMedicamentDisctionary.Add(new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 3, Dose = 80, Details = "" });

            builder.HasData(prescriptionMedicamentDisctionary);
        }

    }
}
