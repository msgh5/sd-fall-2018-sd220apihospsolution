using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIHospital.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool HasInsurance { get; set; }

        public List<VisitViewModel> Visits { get; set; }

        public PatientViewModel()
        {
            Visits = new List<VisitViewModel>();
        }
    }
}