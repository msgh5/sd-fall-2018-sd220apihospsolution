using APIHospital.Models;
using APIHospital.Models.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Web.Http;

namespace APIHospital.Controllers
{
    [Authorize]
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
                .ProjectTo<PatientViewModel>()
                .ToList();

            return Ok(model);
        }

        public IHttpActionResult Get(int id)
        {
            var model = DbContext
                .Patients
                .Where(p => p.Id == id)
                .ProjectTo<PatientViewModel>()
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

            var patient = Mapper.Map<Patient>(formData);

            DbContext.Patients.Add(patient);
            DbContext.SaveChanges();

            var model = Mapper.Map<PatientViewModel>(patient);

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

            Mapper.Map(formData, patient);

            DbContext.SaveChanges();

            var model = Mapper.Map<PatientViewModel>(patient);

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

            var visit = Mapper.Map<Visit>(formData);
            visit.Date = DateTime.Now;

            patient.Visits.Add(visit);

            DbContext.SaveChanges();

            return Ok();
        }
    }
}
