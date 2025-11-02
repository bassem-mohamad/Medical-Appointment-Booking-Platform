using Api_Project.DTOs.Location;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class LocationService 
    {
        private readonly UnitWork unitWork;

        public LocationService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<LocationDto> GetAllLocations()
        {
            var locations = unitWork.LocationRepo.GetAll();
            var locationsDto = new List<LocationDto>();
            foreach (var location in locations)
            {
                locationsDto.Add(new LocationDto
                {
                    Id = location.Id,
                    City = location.City,
                    Area = location.Area,
                });
            }
            return locationsDto;
        }
        public LocationDto? GetLocationById(int id)
        {
            var location = unitWork.LocationRepo.GetById(id);
            if (location == null)
                return null;
            return new LocationDto
            {
                Id = location.Id,
                City = location.City,
                Area = location.Area,
            };
        }

        public LocationDto? GetLocationByName(string name)
        {
            var location = unitWork.LocationRepo.GetAll().FirstOrDefault(l => l.City == name || l.Area == name);
            if (location == null)
                return null;
            return new LocationDto
            {
                Id = location.Id,
                City = location.City,
                Area = location.Area,
            };
        }

        public void Create(CreateLocationDto locationDto)
        {
            var location = new Api_Project.Models.Location
            {
                City = locationDto.City,
                Area = locationDto.Area,
            };
            unitWork.LocationRepo.Add(location);
            unitWork.Save();
        }

        public bool Update(int id, CreateLocationDto locationDto)
        {
            var location = unitWork.LocationRepo.GetById(id);
            if (location == null)
                return false;
            location.City = locationDto.City;
            location.Area = locationDto.Area;
            unitWork.LocationRepo.Update(location);
            unitWork.Save();
            return true;
        }
        public bool Delete(int id)
        {
            var location = unitWork.LocationRepo.GetById(id);
            if (location == null)
                return false;
            unitWork.LocationRepo.Delete(location.Id);
            unitWork.Save();
            return true;
        }
    }
}
