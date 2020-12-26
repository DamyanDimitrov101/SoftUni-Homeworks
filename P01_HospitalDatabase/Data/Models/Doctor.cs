using P01_HospitalDatabase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Visitations = new HashSet<Visitation>();
        }

        public int DoctorId { get; set; }

        [MaxLength(DataCommon.DoctorNameAndSpecialtyMax)]
        public string Name { get; set; }

        [MaxLength(DataCommon.DoctorNameAndSpecialtyMax)]
        public string Specialty { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}
