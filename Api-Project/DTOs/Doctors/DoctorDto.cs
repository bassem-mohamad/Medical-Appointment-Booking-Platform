using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api_Project.Models;

namespace Api_Project.DTOs.Doctors
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Specialty { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal Rating { get; set; }
        public decimal ConsultationFee { get; set; }
        public string ProfileImage { get; set; }
        public string Bio { get; set; }
    }
}
