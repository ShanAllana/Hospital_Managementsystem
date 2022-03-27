using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Hospital.Models
{
    [Table ("Patient")]
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Office { get; set; }

        public string Email { get; set; }

        public string telephone { get; set; }

        public string address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int DoctorID { get; set; }

        public string DoctorName { get; set; }

        public int SelectedPatientId { get; set; }
        public IEnumerable<SelectListItem> UserRoles { get; set; }

    }
}