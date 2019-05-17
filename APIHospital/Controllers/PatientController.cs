using APIHospital.Models;
using APIHospital.Models.Domain;
using System;
using System.Linq;
using System.Web.Http;

namespace APIHospital.Controllers
{
   // [Authorize]
    public class PatientController : ApiController
    {
        private ApplicationDbContext DbContext;

        public PatientController()
        {
            DbContext = new ApplicationDbContext();
        }

        public IHttpActionResult Get()
        {
            var model = DbContext
                .Patients
                .Select(p => new PatientViewModel
                {
                    Id = p.Id,
                    DateOfBirth = p.DateOfBirth,
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    HasInsurance = p.HasInsurance,
                    Visits = p.Visits.Select(t => new VisitViewModel
                    {
                        Comments = t.Comments,
                        Date = t.Date,
                        Id = t.Id
                    }).ToList()
                })
                .ToList();

            return Ok(model);
        }

        public IHttpActionResult Get(int id)
        {
            var model = DbContext
                .Patients
                .Where(p => p.Id == id)
                .Select(p => new PatientViewModel
                {
                    Id = p.Id,
                    DateOfBirth = p.DateOfBirth,
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    HasInsurance = p.HasInsurance,
                    Visits = p.Visits.Select(t => new VisitViewModel
                    {
                        Comments = t.Comments,
                        Date = t.Date,
                        Id = t.Id
                    }).ToList()
                })
                .FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        public IHttpActionResult Post(PatientBindingModel formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = new Patient()
            {
                DateOfBirth = formData.DateOfBirth,
                Email = formData.Email,
                FirstName = formData.FirstName,
                HasInsurance = formData.HasInsurance,
                LastName = formData.LastName
            };

            DbContext.Patients.Add(patient);
            DbContext.SaveChanges();

            var model = new PatientViewModel
            {
                Id = patient.Id,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                HasInsurance = patient.HasInsurance
            };

            return Ok(model);
        }

        public IHttpActionResult Put(int id, PatientBindingModel formData)
        {
            var patient = DbContext.Patients.FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            patient.DateOfBirth = formData.DateOfBirth;
            patient.Email = formData.Email;
            patient.FirstName = formData.FirstName;
            patient.HasInsurance = formData.HasInsurance;
            patient.LastName = formData.LastName;

            DbContext.SaveChanges();

            var model = new PatientViewModel
            {
                Id = patient.Id,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                HasInsurance = patient.HasInsurance
            };

            return Ok(model);
        }

        [Route("api/{id:int}/recordvisit")]
        public IHttpActionResult RecordVisit(int id, VisitBindingModel formData)
        {
            var patient = DbContext
                .Patients
                .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            var visit = new Visit
            {
                Date = DateTime.Now,
                Comments = formData?.Comments
            };

            patient.Visits.Add(visit);

            DbContext.SaveChanges();

            return Ok();
        }
    }
}
