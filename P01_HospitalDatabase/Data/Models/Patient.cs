using P01_HospitalDatabase.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        //	PatientId
        //	FirstName(up to 50 characters, unicode)
        //	LastName(up to 50 characters, unicode)
        //	Address(up to 250 characters, unicode)
        //	Email(up to 80 characters, not unicode)
        //	HasInsurance

        public Patient()
        {
            this.Prescriptions = new HashSet<PatientMedicament>();
            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();

        }

        [Key]
        public int PatientId { get; set; }

        [MaxLength(DataCommon.PatientNameMax)]
        public string FirstName { get; set; }

        [MaxLength(DataCommon.PatientNameMax)]
        public string LastName { get; set; }

        [MaxLength(DataCommon.PatientAddressMax)]
        public string Address { get; set; }

        [MaxLength(DataCommon.PatientEmailMax)]
        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }

        public virtual ICollection<Diagnose> Diagnoses { get; set; }
    }
}
