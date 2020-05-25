using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wyklad10.Models
{
    public class PrescriptionMedicament
    {
        [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }

        [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }

        public int? Dose { get; set; }

        public string Details { get; set; }

        public virtual Medicament medicament { get; set; }

        public virtual Prescription prescription { get; set; }
    }
}
