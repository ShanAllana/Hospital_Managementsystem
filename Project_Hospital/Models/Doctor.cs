using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_Hospital.Models
{
    [Table ("Doctor")]
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Office { get; set; }

        public string Email { get; set; }

        public string telephone { get; set; }

        public string address { get; set; }

    }
}