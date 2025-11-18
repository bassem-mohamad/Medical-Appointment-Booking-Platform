using Api_Project.DTOs.TimeSlot;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class TimeSlotService
    {
        private readonly UnitWork unitWork;

        public TimeSlotService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<TimeSlotDto> GetAllTimeSlots()
        {
            var timeSlots = unitWork.TimeSlotRepo.GetAll().ToList();
            var timeSlotDtos = new List<TimeSlotDto>();
            foreach (var timeSlot in timeSlots)
            {
                timeSlotDtos.Add(new TimeSlotDto
                {
                    Id = timeSlot.Id,
                    DoctorId = timeSlot.DoctorId,
                    Date = timeSlot.Date,
                    Time = timeSlot.Time,
                    IsBooked = timeSlot.IsBooked,
                    AppointmentId = timeSlot.AppointmentId,
                });
            }
            return timeSlotDtos;
        }

        public TimeSlotDto? GetTimeSlotById(int id)
        {
            var timeSlot = unitWork.TimeSlotRepo.GetById(id);
            if (timeSlot == null)
                return null;

            return new TimeSlotDto
            {
                Id = timeSlot.Id,
                DoctorId = timeSlot.DoctorId,
                Date = timeSlot.Date,
                Time = timeSlot.Time,
                IsBooked = timeSlot.IsBooked,
                AppointmentId = timeSlot.AppointmentId,
            };
        }

        public List<TimeSlotDto> GetAvailableTimeSlotsByDoctor(int doctorId, DateTime date)
        {
            var timeSlots = unitWork.TimeSlotRepo.GetAll()
                .Where(ts => ts.DoctorId == doctorId && ts.Date == date.Date && !ts.IsBooked)
                .ToList();

            var timeSlotDtos = new List<TimeSlotDto>();
            foreach (var timeSlot in timeSlots)
            {
                timeSlotDtos.Add(new TimeSlotDto
                {
                    Id = timeSlot.Id,
                    DoctorId = timeSlot.DoctorId,
                    Date = timeSlot.Date,
                    Time = timeSlot.Time,
                    IsBooked = timeSlot.IsBooked,
                    AppointmentId = timeSlot.AppointmentId,
                });
            }
            return timeSlotDtos;
        }

        public void Create(CreateTimeSlotDto timeSlotDto)
        {
            var timeSlot = new TimeSlot
            {
                DoctorId = timeSlotDto.DoctorId,
                Date = timeSlotDto.Date,
                Time = timeSlotDto.Time,
                IsBooked = false,
            };
            unitWork.TimeSlotRepo.Add(timeSlot);
            unitWork.Save();
        }

        public bool Update(int id, CreateTimeSlotDto timeSlotDto)
        {
            var timeSlot = unitWork.TimeSlotRepo.GetById(id);
            if (timeSlot == null)
                return false;

            timeSlot.DoctorId = timeSlotDto.DoctorId;
            timeSlot.Date = timeSlotDto.Date;
            timeSlot.Time = timeSlotDto.Time;

            unitWork.TimeSlotRepo.Update(timeSlot);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var timeSlot = unitWork.TimeSlotRepo.GetById(id);
            if (timeSlot == null)
                return false;

            unitWork.TimeSlotRepo.Delete(id);
            unitWork.Save();
            return true;
        }
    }
}
