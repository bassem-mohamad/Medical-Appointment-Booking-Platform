using Api_Project.Context;
using Api_Project.Models;
using Api_Project.Repository;

namespace Api_Project.UnitOfWork
{
    public class UnitWork
    {
        private readonly ApiDbContext _context;

        private GenericRepository<Doctor> _doctorRepository;
        private GenericRepository<Patient> _patientRepository;
        private GenericRepository<Appointment> _appointmentRepository;
        private GenericRepository<DoctorSchedule> _doctorScheduleRepository;
        private GenericRepository<Location> _locationRepository;
        private GenericRepository<Review> _reviewRepository;
        private GenericRepository<Specialty> _specialtyRepository;
        private GenericRepository<TimeSlot> _timeSlotRepository;
        public UnitWork(ApiDbContext _context)
        {
            this._context = _context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public GenericRepository<Doctor> DoctorRepo
        {
            get
            {
                if( _doctorRepository == null)
                    _doctorRepository = new GenericRepository<Doctor>(_context);
                
                return _doctorRepository;
            }
        } 
        public GenericRepository<Patient> PatientRepo
        {
            get
            {
                if(_patientRepository == null)
                    _patientRepository = new GenericRepository<Patient>(_context);

                return _patientRepository;
            }
        } 
        public GenericRepository<Appointment> AppointmentRepo
        {
            get
            {
                if(_appointmentRepository == null)
                    _appointmentRepository = new GenericRepository<Appointment>(_context);

                return _appointmentRepository;
            }
        }
        public GenericRepository<DoctorSchedule> DoctorScheduleRepo
        {
            get
            {
                if (_doctorScheduleRepository == null)
                    _doctorScheduleRepository = new GenericRepository<DoctorSchedule>(_context);

                return _doctorScheduleRepository;
            }
        }
        public GenericRepository<Location> LocationRepo
        {
            get
            {
                if( _locationRepository == null)
                    _locationRepository = new GenericRepository<Location>(_context);

                return _locationRepository;
            }
        }
        public GenericRepository<Review> ReviewRepo
        {
            get
            {
                if( (_reviewRepository == null) )
                    _reviewRepository = new GenericRepository<Review>(_context);

                return _reviewRepository;
            }
        }
        public GenericRepository<Specialty> SpecialtyRepo
        {
            get
            {
                if(_specialtyRepository == null)
                    _specialtyRepository = new GenericRepository<Specialty>(_context);

                return _specialtyRepository;
            }
        }
        public GenericRepository<TimeSlot> TimeSlotRepo
        {
            get
            {
                if( ( _timeSlotRepository == null) )
                    _timeSlotRepository = new GenericRepository<TimeSlot>(_context);

                return _timeSlotRepository;
            }
        }
    }
}
