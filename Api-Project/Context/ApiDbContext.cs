using Api_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api_Project.Context
{
    public class ApiDbContext : IdentityDbContext<AppUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        // ==================== Override SaveChangesAsync ====================
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.UpdatedAt = null;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        // ==================== Model Configuration ====================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Soft Delete Filters
            modelBuilder.Entity<Doctor>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Patient>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Appointment>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Location>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Specialty>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Review>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<TimeSlot>()
                .HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<DoctorSchedule>()
                .HasQueryFilter(e => !e.IsDeleted);

            // ==================== Static Seeding ====================

            var dateTimeNow = new DateTime(2025, 10, 27, 12, 0, 0);

            // 1️⃣ Specialties
            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { Id = 1, Name = "Cardiology", Description = "Heart and blood vessel specialist", CreatedAt = dateTimeNow },
                new Specialty { Id = 2, Name = "Dermatology", Description = "Skin, hair, and nail specialist", CreatedAt = dateTimeNow },
                new Specialty { Id = 3, Name = "Neurology", Description = "Brain and nervous system specialist", CreatedAt = dateTimeNow },
                new Specialty { Id = 4, Name = "Pediatrics", Description = "Child healthcare specialist", CreatedAt = dateTimeNow }
            );

            // 2️⃣ Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, City = "Cairo", Area = "Nasr City", CreatedAt = dateTimeNow },
                new Location { Id = 2, City = "Alexandria", Area = "Smouha", CreatedAt = dateTimeNow },
                new Location { Id = 3, City = "Giza", Area = "Dokki", CreatedAt = dateTimeNow }
            );

            // 3️⃣ Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    Name = "Dr. Ahmed Hassan",
                    SpecialtyId = 1,
                    LocationId = 1,
                    YearsOfExperience = 12,
                    ConsultationFee = 400,
                    Bio = "Specialized in heart surgery and cardiovascular diseases.",
                    ProfileImage = "images/doctors/ahmed.jpg",
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                },
                new Doctor
                {
                    Id = 2,
                    Name = "Dr. Mona Said",
                    SpecialtyId = 2,
                    LocationId = 3,
                    YearsOfExperience = 8,
                    ConsultationFee = 300,
                    Bio = "Expert in skin care and laser treatments.",
                    ProfileImage = "images/doctors/mona.jpg",
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                },
                new Doctor
                {
                    Id = 3,
                    Name = "Dr. Karim Nabil",
                    SpecialtyId = 4,
                    LocationId = 2,
                    YearsOfExperience = 6,
                    ConsultationFee = 250,
                    Bio = "Dedicated to child wellness and vaccination.",
                    ProfileImage = "images/doctors/karim.jpg",
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                }
            );

            // 4️⃣ Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FullName = "Omar Ali",
                    DateOfBirth = new DateTime(1995, 5, 10),
                    Gender = Gender.Male,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                },
                new Patient
                {
                    Id = 2,
                    FullName = "Sara Mohamed",
                    DateOfBirth = new DateTime(2000, 3, 22),
                    Gender = Gender.Female,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                }
            );

            // 5️⃣ TimeSlots
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot
                {
                    Id = 1,
                    DoctorId = 1,
                    Date = new DateTime(2025, 10, 28),
                    Time = new TimeSpan(10, 0, 0),
                    IsBooked = false,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                },
                new TimeSlot
                {
                    Id = 2,
                    DoctorId = 1,
                    Date = new DateTime(2025, 10, 28),
                    Time = new TimeSpan(11, 0, 0),
                    IsBooked = false,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                },
                new TimeSlot
                {
                    Id = 3,
                    DoctorId = 2,
                    Date = new DateTime(2025, 10, 29),
                    Time = new TimeSpan(12, 0, 0),
                    IsBooked = false,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                }
            );

            // 6️⃣ Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    DoctorId = 1,
                    PatientId = 1,
                    AppointmentDate = new DateTime(2025, 10, 28),
                    AppointmentTime = new TimeSpan(10, 0, 0),
                    Status = AppointmentStatus.Confirmed,
                    Fee = 400,
                    Notes = "Follow-up visit",
                    BookedAt = dateTimeNow,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                }
            );

            // 7️⃣ Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    DoctorId = 1,
                    PatientId = 1,
                    AppointmentId = 1,
                    Rating = 5,
                    Comment = "Excellent doctor, very helpful!",
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                }
            );

            // 8️⃣ Doctor Schedules
            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule
                {
                    Id = 1,
                    DoctorId = 1,
                    DayOfWeek = DayOfWeek.Sunday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(15, 0, 0),
                    SlotDurationMinutes = 30,
                    IsAvailable = true,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                },
                new DoctorSchedule
                {
                    Id = 2,
                    DoctorId = 2,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    SlotDurationMinutes = 30,
                    IsAvailable = true,
                    IsDeleted = false,
                    CreatedAt = dateTimeNow
                }
            );
        }
    }
}
