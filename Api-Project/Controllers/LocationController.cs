using Api_Project.DTOs.Location;
using Api_Project.Models;
using Api_Project.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly UnitWork unitWork;
        public LocationController(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        [HttpGet]
        public ActionResult GetLocations()
        {
            var locations = unitWork.LocationRepo.GetAll();
            var locationsDto = new List<LocationDto>();
            {
                foreach (var location in locations)
                {
                    locationsDto.Add(new LocationDto
                    {
                        Id = location.Id,
                        City = location.City,
                        Area = location.Area,
                    });

                }
            }
            return Ok(locationsDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public ActionResult GetLocation(int id)
        {
            var location = unitWork.LocationRepo.GetById(id);
            if (location == null) 
                return BadRequest("Not Found");
            var LocationDto = new LocationDto
            {
                Id = location.Id,
                City = location.City,
                Area = location.Area,
            };
            return Ok(LocationDto);
        }

        [HttpGet("ByName/{name}")]
        public ActionResult GetByname(string name)
        {
            var location = unitWork.LocationRepo.GetAll().FirstOrDefault(l=> l.City==name|| l.Area == name);
            if (location == null)
                return BadRequest("Not Found");

            var LocationsDto = new LocationDto
            {
                Id = location.Id,
                City = location.City,
                Area = location.Area,
            };
            return Ok(LocationsDto);
        } 

        [HttpPost]
        public ActionResult Create(CreateLocationDto dto)
        {
            if (dto == null)
                return BadRequest("Data Null");
            var location = new Location
            {
                City = dto.City,
                Area = dto.Area,
            };
            unitWork.LocationRepo.Add(location);
            unitWork.Save();
            return Created();
        }

        [HttpPut]
        public ActionResult Update(int id, CreateLocationDto dto)
        {
            var location = unitWork.LocationRepo.GetById(id);
            if (location == null)
                return BadRequest("Id Not Found");
            if (dto == null)
                return BadRequest("Data null");
            location.City = dto.City;
            location.Area = dto.Area;

            unitWork.Save();
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var location = unitWork.LocationRepo.GetById(id);
            if (location == null)
                return BadRequest("Not found");
            unitWork.LocationRepo.Delete(location.Id);
            unitWork.Save();
            return NoContent();
        }
    }
}
