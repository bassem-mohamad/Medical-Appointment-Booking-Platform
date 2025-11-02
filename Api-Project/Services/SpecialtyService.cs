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
    }
}
