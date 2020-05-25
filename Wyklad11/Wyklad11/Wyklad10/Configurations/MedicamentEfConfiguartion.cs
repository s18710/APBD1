using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wyklad10.Models;

namespace Wyklad10.Configurations
{
    public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            var medicamentDictionary = new List<Medicament>();
            medicamentDictionary.Add(new Medicament {IdMedicament = 1, Name = "Telfexo", Description = "na alergie", Type = "alergia" });
            medicamentDictionary.Add(new Medicament { IdMedicament = 2, Name = "Comboterol", Description = "na astme", Type = "astma" });
            medicamentDictionary.Add(new Medicament { IdMedicament = 3, Name = "Flixotide", Description = "na astme", Type = "astma" });
            medicamentDictionary.Add(new Medicament { IdMedicament = 4, Name = "Ella One", Description = "tabletka dzien po", Type = "antykoncepcja" });

            builder.HasData(medicamentDictionary);
        }
    }
}
