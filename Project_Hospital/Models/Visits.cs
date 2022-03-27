using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Hospital.Models
{
    [Table ("Visits")]
    public class Visits
    {
        public int Id { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        
        public DateTime DateOfVisit { get; set; }
        
        public string Complaint { get; set; }


        public string DoctorName { get; set; }
        public string PatientName { get; set; }

    }
}