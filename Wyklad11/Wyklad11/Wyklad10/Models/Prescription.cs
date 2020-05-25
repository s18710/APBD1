using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wyklad10.Models
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        [ForeignKey("Patient")]
        public int IdPatient { get; set; }

        [ForeignKey("Doctor")]
        public int IdDoctor { get; set; }

        public virtual Patient patient { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedciaments { get; set; }
    }
}
