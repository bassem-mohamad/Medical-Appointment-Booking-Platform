using Api_Project.DTOs.Doctors;
using Api_Project.Models;
using Api_Project.Services;
using Api_Project.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly UnitWork unitWork;
        private readonly DoctorService doctorService;

        public DoctorsController(UnitWork unitWork, DoctorService doctorService)
        {
            this.unitWork = unitWork;
            this.doctorService = doctorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDoctors()
        {
            try
            {
                var doctors = doctorService.GetAllDoctors();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving doctors", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public ActionResult GetDoctor(int id)
        {
            try
            {
                var doctor = doctorService.GetDoctorById(id);
                if (doctor == null)
                    return NotFound(new { message = "Doctor not found" });

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving doctor", error = ex.Message });
            }
        }

        [HttpPost("Search")]
        [AllowAnonymous]
        public ActionResult Search(SearchDoctorDto searchDto)
        {
            try
            {
                var doctors = doctorService.SearchDoctors(searchDto);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searching doctors", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(CreateDoctorDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                doctorService.Create(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating doctor", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, CreateDoctorDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = doctorService.Update(id, dto);
                if (!result)
                    return NotFound(new { message = "Doctor not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating doctor", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = doctorService.Delete(id);
                if (!result)
                    return NotFound(new { message = "Doctor not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting doctor", error = ex.Message });
            }
        }
    }
}
