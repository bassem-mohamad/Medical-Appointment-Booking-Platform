using Api_Project.DTOs.Appointment;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class AppointmentService
    {
        private readonly UnitWork unitWork;

        public AppointmentService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<AppointmentDto> GetAllAppointments()
        {
            var appointments = unitWork.AppointmentRepo.GetAll().ToList();
            var appointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointments)
            {
                appointmentDtos.Add(new AppointmentDto
                {
                    Id = appointment.Id,
                    DoctorName = appointment.Doctor?.Name,
                    PatientName = appointment.Patient?.FullName,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status,
                    Notes = appointment.Notes,
                    Fee = appointment.Fee,
                    BookedAt = appointment.BookedAt,
                });
            }
            return appointmentDtos;
        }

        public AppointmentDto? GetAppointmentById(int id)
        {
            var appointment = unitWork.AppointmentRepo.GetById(id);
            if (appointment == null)
                return null;

            return new AppointmentDto
            {
                Id = appointment.Id,
                DoctorName = appointment.Doctor?.Name,
                PatientName = appointment.Patient?.FullName,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Status = appointment.Status,
                Notes = appointment.Notes,
                Fee = appointment.Fee,
                BookedAt = appointment.BookedAt,
            };
        }

        public List<AppointmentDto> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = unitWork.AppointmentRepo.GetAll()
                .Where(a => a.DoctorId == doctorId)
                .ToList();

            var appointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointments)
            {
                appointmentDtos.Add(new AppointmentDto
                {
                    Id = appointment.Id,
                    DoctorName = appointment.Doctor?.Name,
                    PatientName = appointment.Patient?.FullName,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status,
                    Notes = appointment.Notes,
                    Fee = appointment.Fee,
                    BookedAt = appointment.BookedAt,
                });
            }
            return appointmentDtos;
        }

        public List<AppointmentDto> GetAppointmentsByPatient(int patientId)
        {
            var appointments = unitWork.AppointmentRepo.GetAll()
                .Where(a => a.PatientId == patientId)
                .ToList();

            var appointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointments)
            {
                appointmentDtos.Add(new AppointmentDto
                {
                    Id = appointment.Id,
                    DoctorName = appointment.Doctor?.Name,
                    PatientName = appointment.Patient?.FullName,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime,
                    Status = appointment.Status,
                    Notes = appointment.Notes,
                    Fee = appointment.Fee,
                    BookedAt = appointment.BookedAt,
                });
            }
            return appointmentDtos;
        }

        public bool Create(int patientId, CreateAppointmentDto appointmentDto)
        {
            var doctor = unitWork.DoctorRepo.GetById(appointmentDto.DoctorId);
            if (doctor == null)
                return false;

            var patient = unitWork.PatientRepo.GetById(patientId);
            if (patient == null)
                return false;

            // ?????? ?? ??? ???? ??? ?? ??? ??????
            var existingAppointment = unitWork.AppointmentRepo.GetAll()
                .FirstOrDefault(a => a.DoctorId == appointmentDto.DoctorId &&
                                     a.AppointmentDate == appointmentDto.AppointmentDate &&
                                     a.AppointmentTime == appointmentDto.AppointmentTime &&
                                     a.Status != AppointmentStatus.Cancelled);
            if (existingAppointment != null)
                return false;

            var appointment = new Appointment
            {
                DoctorId = appointmentDto.DoctorId,
                PatientId = patientId,
                AppointmentDate = appointmentDto.AppointmentDate,
                AppointmentTime = appointmentDto.AppointmentTime,
                Status = AppointmentStatus.Pending,
                Notes = appointmentDto.Notes,
                Fee = doctor.ConsultationFee,
                BookedAt = DateTime.Now,
            };

            unitWork.AppointmentRepo.Add(appointment);
            unitWork.Save();
            return true;
        }

        public bool Update(int id, UpdateAppointmentDto appointmentDto)
        {
            var appointment = unitWork.AppointmentRepo.GetById(id);
            if (appointment == null)
                return false;

            appointment.Status = appointmentDto.Status;
            appointment.Notes = appointmentDto.Notes ?? appointment.Notes;

            if (appointmentDto.Status == AppointmentStatus.Cancelled)
            {
                appointment.CancelledAt = DateTime.Now;
                appointment.CancellationReason = appointmentDto.CancellationReason;
            }

            unitWork.AppointmentRepo.Update(appointment);
            unitWork.Save();
            return true;
        }

        public bool Cancel(int id, string? reason)
        {
            var appointment = unitWork.AppointmentRepo.GetById(id);
            if (appointment == null)
                return false;

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancelledAt = DateTime.Now;
            appointment.CancellationReason = reason;

            unitWork.AppointmentRepo.Update(appointment);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var appointment = unitWork.AppointmentRepo.GetById(id);
            if (appointment == null)
                return false;

            unitWork.AppointmentRepo.Delete(id);
            unitWork.Save();
            return true;
        }
    }
}
