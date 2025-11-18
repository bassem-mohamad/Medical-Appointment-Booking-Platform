using Api_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
