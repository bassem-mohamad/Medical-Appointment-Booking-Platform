using Api_Project.DTOs.DoctorSchedule;
using Api_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorSchedulesController : ControllerBase
    {
        private readonly DoctorScheduleService scheduleService;

        public DoctorSchedulesController(DoctorScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [HttpGet]
        public ActionResult GetSchedules()
        {
            try
            {
                var schedules = scheduleService.GetAllSchedules();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving schedules", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult GetSchedule(int id)
        {
            try
            {
                var schedule = scheduleService.GetScheduleById(id);
                if (schedule == null)
                    return NotFound(new { message = "Schedule not found" });

                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving schedule", error = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId:int}")]
        [AllowAnonymous]
        public ActionResult GetSchedulesByDoctor(int doctorId)
        {
            try
            {
                var schedules = scheduleService.GetSchedulesByDoctor(doctorId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving doctor schedules", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(CreateDoctorScheduleDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                scheduleService.Create(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating schedule", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, CreateDoctorScheduleDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = scheduleService.Update(id, dto);
                if (!result)
                    return NotFound(new { message = "Schedule not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating schedule", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = scheduleService.Delete(id);
                if (!result)
                    return NotFound(new { message = "Schedule not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting schedule", error = ex.Message });
            }
        }
    }
}
