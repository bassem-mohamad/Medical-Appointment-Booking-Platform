using Api_Project.DTOs.Doctors;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class DoctorService
    {
        private readonly UnitWork unitWork;

        public DoctorService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<DoctorDto> GetAllDoctors()
        {
            var doctors = unitWork.DoctorRepo.GetAll().ToList();
            var doctorDtos = new List<DoctorDto>();
            foreach (var doctor in doctors)
            {
                doctorDtos.Add(new DoctorDto
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Specialty = doctor.Specialty?.Name,
                    City = doctor.Location?.City,
                    Area = doctor.Location?.Area,
                    YearsOfExperience = doctor.YearsOfExperience,
                    ConsultationFee = doctor.ConsultationFee,
                    ProfileImage = doctor.ProfileImage,
                    Bio = doctor.Bio,
                });
            }
            return doctorDtos;
        }

        public DoctorDto? GetDoctorById(int id)
        {
            var doctor = unitWork.DoctorRepo.GetById(id);
            if (doctor == null)
                return null;

            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty?.Name,
                City = doctor.Location?.City,
                Area = doctor.Location?.Area,
                YearsOfExperience = doctor.YearsOfExperience,
                ConsultationFee = doctor.ConsultationFee,
                ProfileImage = doctor.ProfileImage,
                Bio = doctor.Bio,
            };
        }

        public List<DoctorDto> SearchDoctors(SearchDoctorDto searchDto)
        {
            var allDoctors = unitWork.DoctorRepo.GetAll().ToList();

            if (searchDto.SpecialtyId.HasValue)
                allDoctors = allDoctors.Where(d => d.SpecialtyId == searchDto.SpecialtyId).ToList();

            if (searchDto.LocationId.HasValue)
                allDoctors = allDoctors.Where(d => d.LocationId == searchDto.LocationId).ToList();

            if (!string.IsNullOrWhiteSpace(searchDto.SearchTerm))
                allDoctors = allDoctors.Where(d => d.Name.Contains(searchDto.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            var doctorDtos = new List<DoctorDto>();
            foreach (var doctor in allDoctors)
            {
                doctorDtos.Add(new DoctorDto
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Specialty = doctor.Specialty?.Name,
                    City = doctor.Location?.City,
                    Area = doctor.Location?.Area,
                    YearsOfExperience = doctor.YearsOfExperience,
                    ConsultationFee = doctor.ConsultationFee,
                    ProfileImage = doctor.ProfileImage,
                    Bio = doctor.Bio,
                });
            }
            return doctorDtos;
        }

        public void Create(CreateDoctorDto doctorDto)
        {
            var doctor = new Doctor
            {
                Name = doctorDto.Name,
                SpecialtyId = doctorDto.SpecialtyId,
                LocationId = doctorDto.LocationId,
                YearsOfExperience = doctorDto.YearsOfExperience,
                ConsultationFee = doctorDto.ConsultationFee,
                ProfileImage = doctorDto.ProfileImage,
                Bio = doctorDto.Bio
            };
            unitWork.DoctorRepo.Add(doctor);
            unitWork.Save();
        }

        public bool Update(int id, CreateDoctorDto doctorDto)
        {
            var doctor = unitWork.DoctorRepo.GetById(id);
            if (doctor == null)
                return false;

            doctor.Name = doctorDto.Name;
            doctor.SpecialtyId = doctorDto.SpecialtyId;
            doctor.LocationId = doctorDto.LocationId;
            doctor.YearsOfExperience = doctorDto.YearsOfExperience;
            doctor.ConsultationFee = doctorDto.ConsultationFee;
            doctor.ProfileImage = doctorDto.ProfileImage;
            doctor.Bio = doctorDto.Bio;

            unitWork.DoctorRepo.Update(doctor);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var doctor = unitWork.DoctorRepo.GetById(id);
            if (doctor == null)
                return false;

            unitWork.DoctorRepo.Delete(id);
            unitWork.Save();
            return true;
        }
    }
}
