using Api_Project.DTOs.TimeSlot;
using Api_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeSlotsController : ControllerBase
    {
        private readonly TimeSlotService timeSlotService;

        public TimeSlotsController(TimeSlotService timeSlotService)
        {
            this.timeSlotService = timeSlotService;
        }

        [HttpGet]
        public ActionResult GetTimeSlots()
        {
            try
            {
                var timeSlots = timeSlotService.GetAllTimeSlots();
                return Ok(timeSlots);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving time slots", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult GetTimeSlot(int id)
        {
            try
            {
                var timeSlot = timeSlotService.GetTimeSlotById(id);
                if (timeSlot == null)
                    return NotFound(new { message = "Time slot not found" });

                return Ok(timeSlot);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving time slot", error = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId:int}/available")]
        [AllowAnonymous]
        public ActionResult GetAvailableTimeSlots(int doctorId, [FromQuery] DateTime date)
        {
            try
            {
                var timeSlots = timeSlotService.GetAvailableTimeSlotsByDoctor(doctorId, date);
                return Ok(timeSlots);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving available time slots", error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(CreateTimeSlotDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                timeSlotService.Create(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating time slot", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, CreateTimeSlotDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = timeSlotService.Update(id, dto);
                if (!result)
                    return NotFound(new { message = "Time slot not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating time slot", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = timeSlotService.Delete(id);
                if (!result)
                    return NotFound(new { message = "Time slot not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting time slot", error = ex.Message });
            }
        }
    }
}
