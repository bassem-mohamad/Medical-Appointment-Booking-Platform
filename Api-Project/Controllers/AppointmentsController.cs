using Api_Project.DTOs.Appointment;
using Api_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult GetAppointments()
        {
            try
            {
                var appointments = appointmentService.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving appointments", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult GetAppointment(int id)
        {
            try
            {
                var appointment = appointmentService.GetAppointmentById(id);
                if (appointment == null)
                    return NotFound(new { message = "Appointment not found" });

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving appointment", error = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId:int}")]
        public ActionResult GetAppointmentsByDoctor(int doctorId)
        {
            try
            {
                var appointments = appointmentService.GetAppointmentsByDoctor(doctorId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving doctor appointments", error = ex.Message });
            }
        }

        [HttpGet("patient/{patientId:int}")]
        public ActionResult GetAppointmentsByPatient(int patientId)
        {
            try
            {
                var appointments = appointmentService.GetAppointmentsByPatient(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving patient appointments", error = ex.Message });
            }
        }

        [HttpPost("patient/{patientId:int}")]
        public ActionResult Create(int patientId, CreateAppointmentDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = appointmentService.Create(patientId, dto);
                if (!result)
                    return BadRequest(new { message = "Failed to create appointment. Doctor may not exist or slot may be booked." });

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating appointment", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, UpdateAppointmentDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = appointmentService.Update(id, dto);
                if (!result)
                    return NotFound(new { message = "Appointment not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating appointment", error = ex.Message });
            }
        }

        [HttpPut("{id:int}/cancel")]
        public ActionResult Cancel(int id, [FromBody] CancelAppointmentDto dto)
        {
            try
            {
                var result = appointmentService.Cancel(id, dto?.Reason);
                if (!result)
                    return NotFound(new { message = "Appointment not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error cancelling appointment", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = appointmentService.Delete(id);
                if (!result)
                    return NotFound(new { message = "Appointment not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting appointment", error = ex.Message });
            }
        }
    }

    public class CancelAppointmentDto
    {
        public string? Reason { get; set; }
    }
}
