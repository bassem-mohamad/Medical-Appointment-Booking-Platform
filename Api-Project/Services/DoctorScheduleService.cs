using Api_Project.DTOs.DoctorSchedule;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class DoctorScheduleService
    {
        private readonly UnitWork unitWork;

        public DoctorScheduleService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<DoctorScheduleDto> GetAllSchedules()
        {
            var schedules = unitWork.DoctorScheduleRepo.GetAll().ToList();
            var scheduleDtos = new List<DoctorScheduleDto>();
            foreach (var schedule in schedules)
            {
                scheduleDtos.Add(new DoctorScheduleDto
                {
                    Id = schedule.Id,
                    DoctorId = schedule.DoctorId,
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    SlotDurationMinutes = schedule.SlotDurationMinutes,
                    IsAvailable = schedule.IsAvailable,
                });
            }
            return scheduleDtos;
        }

        public DoctorScheduleDto? GetScheduleById(int id)
        {
            var schedule = unitWork.DoctorScheduleRepo.GetById(id);
            if (schedule == null)
                return null;

            return new DoctorScheduleDto
            {
                Id = schedule.Id,
                DoctorId = schedule.DoctorId,
                DayOfWeek = schedule.DayOfWeek,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                SlotDurationMinutes = schedule.SlotDurationMinutes,
                IsAvailable = schedule.IsAvailable,
            };
        }

        public List<DoctorScheduleDto> GetSchedulesByDoctor(int doctorId)
        {
            var schedules = unitWork.DoctorScheduleRepo.GetAll()
                .Where(s => s.DoctorId == doctorId)
                .ToList();

            var scheduleDtos = new List<DoctorScheduleDto>();
            foreach (var schedule in schedules)
            {
                scheduleDtos.Add(new DoctorScheduleDto
                {
                    Id = schedule.Id,
                    DoctorId = schedule.DoctorId,
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    SlotDurationMinutes = schedule.SlotDurationMinutes,
                    IsAvailable = schedule.IsAvailable,
                });
            }
            return scheduleDtos;
        }

        public void Create(CreateDoctorScheduleDto scheduleDto)
        {
            var schedule = new DoctorSchedule
            {
                DoctorId = scheduleDto.DoctorId,
                DayOfWeek = scheduleDto.DayOfWeek,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime,
                SlotDurationMinutes = scheduleDto.SlotDurationMinutes,
                IsAvailable = scheduleDto.IsAvailable,
            };
            unitWork.DoctorScheduleRepo.Add(schedule);
            unitWork.Save();
        }

        public bool Update(int id, CreateDoctorScheduleDto scheduleDto)
        {
            var schedule = unitWork.DoctorScheduleRepo.GetById(id);
            if (schedule == null)
                return false;

            schedule.DoctorId = scheduleDto.DoctorId;
            schedule.DayOfWeek = scheduleDto.DayOfWeek;
            schedule.StartTime = scheduleDto.StartTime;
            schedule.EndTime = scheduleDto.EndTime;
            schedule.SlotDurationMinutes = scheduleDto.SlotDurationMinutes;
            schedule.IsAvailable = scheduleDto.IsAvailable;

            unitWork.DoctorScheduleRepo.Update(schedule);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var schedule = unitWork.DoctorScheduleRepo.GetById(id);
            if (schedule == null)
                return false;

            unitWork.DoctorScheduleRepo.Delete(id);
            unitWork.Save();
            return true;
        }
    }
}
