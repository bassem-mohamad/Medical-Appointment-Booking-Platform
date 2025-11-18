using Api_Project.DTOs.Patient;
using Api_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService patientService;

        public PatientsController(PatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpGet]
        public ActionResult GetPatients()
        {
            try
            {
                var patients = patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving patients", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult GetPatient(int id)
        {
            try
            {
                var patient = patientService.GetPatientById(id);
                if (patient == null)
                    return NotFound(new { message = "Patient not found" });

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving patient", error = ex.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(CreatePatientDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                patientService.Create(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating patient", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, CreatePatientDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = patientService.Update(id, dto);
                if (!result)
                    return NotFound(new { message = "Patient not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating patient", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = patientService.Delete(id);
                if (!result)
                    return NotFound(new { message = "Patient not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting patient", error = ex.Message });
            }
        }
    }
}
