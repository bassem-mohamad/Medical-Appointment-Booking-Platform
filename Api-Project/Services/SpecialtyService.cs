using Api_Project.DTOs.Specialty;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class SpecialtyService
    {
        private readonly UnitWork unitWork;

        public SpecialtyService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<SpecialtyDto> GetAllSpecialties()
        {
            var specialties = unitWork.SpecialtyRepo.GetAll().ToList();
            var specialtyDtos = new List<SpecialtyDto>();
            foreach (var specialty in specialties)
            {
                specialtyDtos.Add(new SpecialtyDto
                {
                    Id = specialty.Id,
                    Name = specialty.Name,
                    Description = specialty.Description,
                });
            }
            return specialtyDtos;
        }

        public SpecialtyDto? GetSpecialtyById(int id)
        {
            var specialty = unitWork.SpecialtyRepo.GetById(id);
            if (specialty == null)
                return null;

            return new SpecialtyDto
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description,
            };
        }

        public SpecialtyDto? GetSpecialtyByName(string name)
        {
            var specialty = unitWork.SpecialtyRepo.GetAll()
                .FirstOrDefault(s => s.Name == name);
            if (specialty == null)
                return null;

            return new SpecialtyDto
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description,
            };
        }

        public void Create(CreateSpecialtyDto specialtyDto)
        {
            var specialty = new Specialty
            {
                Name = specialtyDto.Name,
                Description = specialtyDto.Description,
            };
            unitWork.SpecialtyRepo.Add(specialty);
            unitWork.Save();
        }

        public bool Update(int id, CreateSpecialtyDto specialtyDto)
        {
            var specialty = unitWork.SpecialtyRepo.GetById(id);
            if (specialty == null)
                return false;

            specialty.Name = specialtyDto.Name;
            specialty.Description = specialtyDto.Description;

            unitWork.SpecialtyRepo.Update(specialty);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var specialty = unitWork.SpecialtyRepo.GetById(id);
            if (specialty == null)
                return false;

            unitWork.SpecialtyRepo.Delete(id);
            unitWork.Save();
            return true;
        }
    }
}
