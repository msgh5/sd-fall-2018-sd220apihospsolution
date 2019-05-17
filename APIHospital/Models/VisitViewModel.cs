using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIHospital.Models
{
    public class VisitViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
    }
}