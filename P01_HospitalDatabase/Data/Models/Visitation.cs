using P01_HospitalDatabase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Visitation
    {
        //•	Visitation:
        //	VisitationId
        //	Date
        //	Comments(up to 250 characters, unicode)
        //	Patient

        [Key]
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(DataCommon.VisitationCommentsMax)]
        public string Comments { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
