using Api_Project.DTOs.Specialty;
using Api_Project.Models;
using Api_Project.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        UnitWork unitWork;
        public SpecialtyController(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        [HttpGet]
        public ActionResult<List<SpecialtyDto>> GetSpecialty()
        {
            var specialties = unitWork.SpecialtyRepo.GetAll();
            var SpecialtyDto = new List<SpecialtyDto>();
            {
                foreach (var specialty in specialties)
                {
                    SpecialtyDto.Add(new SpecialtyDto
                    {
                        Id = specialty.Id,
                        Name = specialty.Name,
                        Description = specialty.Description
                    });
                }
            }
            return Ok(SpecialtyDto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SpecialtyDto> GetSpecialty(int id)
        {
            var specialty = unitWork.SpecialtyRepo.GetById(id);
            if (specialty == null)
                return BadRequest("Not Found");
            var SpecialtyDto = new SpecialtyDto
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description
            };
            return Ok(SpecialtyDto);
        }

        [HttpGet("ByName/{name}")]
        public ActionResult<SpecialtyDto> GetByName(string name)
        {
            var specialty = unitWork.SpecialtyRepo.GetAll().FirstOrDefault(x => x.Name == name);
            if (specialty == null)
                return BadRequest("Not Found");
            var specialtyDto = new SpecialtyDto
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description
            };
            return Ok(specialtyDto);
        }

        [HttpPost]
        public ActionResult Create(CreateSpecialtyDto dto)
        {
            if (dto == null)
                return BadRequest("Data Null");

            var specialty = new Specialty
            {
                Name = dto.Name,
                Description = dto.Description
            };
            unitWork.SpecialtyRepo.Add(specialty);
            unitWork.Save();  
            return Created();
        }

        [HttpPut]
        public ActionResult Update(int id ,CreateSpecialtyDto dto)
        {
            var specialty = unitWork.SpecialtyRepo.GetById(id);
            if (specialty == null)
                return BadRequest("Not Found");

            if (dto == null)
                return BadRequest("Data Null");

            specialty.Name = dto.Name;
            specialty.Description = dto.Description;

            unitWork.Save();
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var specialty = unitWork.SpecialtyRepo.GetById(id);
            if (specialty == null)
                return BadRequest("Not Found");
            unitWork.SpecialtyRepo.Delete(specialty.Id);
            unitWork.Save();
            return NoContent();
        }
    }
}
