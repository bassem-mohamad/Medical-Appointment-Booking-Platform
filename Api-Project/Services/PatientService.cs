using Api_Project.DTOs.Patient;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class PatientService
    {
        private readonly UnitWork unitWork;

        public PatientService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<PatientDto> GetAllPatients()
        {
            var patients = unitWork.PatientRepo.GetAll().ToList();
            var patientDtos = new List<PatientDto>();
            foreach (var patient in patients)
            {
                patientDtos.Add(new PatientDto
                {
                    Id = patient.Id,
                    FullName = patient.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                });
            }
            return patientDtos;
        }

        public PatientDto? GetPatientById(int id)
        {
            var patient = unitWork.PatientRepo.GetById(id);
            if (patient == null)
                return null;

            return new PatientDto
            {
                Id = patient.Id,
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
            };
        }

        public void Create(CreatePatientDto patientDto)
        {
            var patient = new Patient
            {
                FullName = patientDto.FullName,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender,
            };
            unitWork.PatientRepo.Add(patient);
            unitWork.Save();
        }

        public bool Update(int id, CreatePatientDto patientDto)
        {
            var patient = unitWork.PatientRepo.GetById(id);
            if (patient == null)
                return false;

            patient.FullName = patientDto.FullName;
            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.Gender = patientDto.Gender;

            unitWork.PatientRepo.Update(patient);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var patient = unitWork.PatientRepo.GetById(id);
            if (patient == null)
                return false;

            unitWork.PatientRepo.Delete(id);
            unitWork.Save();
            return true;
        }
    }
}
