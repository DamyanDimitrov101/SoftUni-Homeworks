using P01_HospitalDatabase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        //•	Medicament:
        //MedicamentId
        //Name(up to 50 characters, unicode)

        public Medicament()
        {
            this.Prescriptions = new HashSet<PatientMedicament>();
        }

        [Key]
        public int MedicamentId { get; set; }

        [MaxLength(DataCommon.MedicamentNameMax)]
        public string Name { get; set; }


        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
