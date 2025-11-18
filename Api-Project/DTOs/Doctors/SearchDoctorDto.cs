namespace Api_Project.DTOs.Doctors
{
    public class SearchDoctorDto
    {
        public int? SpecialtyId { get; set; }
        public int? LocationId { get; set; }
        public string SearchTerm { get; set; }
    }
}
