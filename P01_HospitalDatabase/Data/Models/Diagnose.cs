using P01_HospitalDatabase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        //•	Diagnose:
        //	DiagnoseId
        //	Name(up to 50 characters, unicode)
        //	Comments(up to 250 characters, unicode)
        //	Patient

        [Key]
        public int DiagnoseId { get; set; }

        [MaxLength(DataCommon.DiagnoseNameMax)]
        public string Name { get; set; }

        [MaxLength(DataCommon.DiagnoseCommentsMax)]
        public string Comments { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
