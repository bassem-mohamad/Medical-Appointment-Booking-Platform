using Api_Project.DTOs.Review;
using Api_Project.Models;
using Api_Project.UnitOfWork;

namespace Api_Project.Services
{
    public class ReviewService
    {
        private readonly UnitWork unitWork;

        public ReviewService(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        public List<ReviewDto> GetAllReviews()
        {
            var reviews = unitWork.ReviewRepo.GetAll().ToList();
            var reviewDtos = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDtos.Add(new ReviewDto
                {
                    Id = review.Id,
                    DoctorId = review.DoctorId,
                    PatientId = review.PatientId,
                    AppointmentId = review.AppointmentId,
                    Rating = review.Rating,
                    Comment = review.Comment,
                });
            }
            return reviewDtos;
        }

        public ReviewDto? GetReviewById(int id)
        {
            var review = unitWork.ReviewRepo.GetById(id);
            if (review == null)
                return null;

            return new ReviewDto
            {
                Id = review.Id,
                DoctorId = review.DoctorId,
                PatientId = review.PatientId,
                AppointmentId = review.AppointmentId,
                Rating = review.Rating,
                Comment = review.Comment,
            };
        }

        public List<ReviewDto> GetReviewsByDoctor(int doctorId)
        {
            var reviews = unitWork.ReviewRepo.GetAll()
                .Where(r => r.DoctorId == doctorId)
                .ToList();

            var reviewDtos = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDtos.Add(new ReviewDto
                {
                    Id = review.Id,
                    DoctorId = review.DoctorId,
                    PatientId = review.PatientId,
                    AppointmentId = review.AppointmentId,
                    Rating = review.Rating,
                    Comment = review.Comment,
                });
            }
            return reviewDtos;
        }

        public List<ReviewDto> GetReviewsByPatient(int patientId)
        {
            var reviews = unitWork.ReviewRepo.GetAll()
                .Where(r => r.PatientId == patientId)
                .ToList();

            var reviewDtos = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                reviewDtos.Add(new ReviewDto
                {
                    Id = review.Id,
                    DoctorId = review.DoctorId,
                    PatientId = review.PatientId,
                    AppointmentId = review.AppointmentId,
                    Rating = review.Rating,
                    Comment = review.Comment,
                });
            }
            return reviewDtos;
        }

        public bool Create(int patientId, CreateReviewDto reviewDto)
        {
            var appointment = unitWork.AppointmentRepo.GetById(reviewDto.AppointmentId);
            if (appointment == null || appointment.PatientId != patientId)
                return false;

            var existingReview = unitWork.ReviewRepo.GetAll()
                .FirstOrDefault(r => r.AppointmentId == reviewDto.AppointmentId);
            if (existingReview != null)
                return false;

            var review = new Review
            {
                DoctorId = reviewDto.DoctorId,
                PatientId = patientId,
                AppointmentId = reviewDto.AppointmentId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
            };

            unitWork.ReviewRepo.Add(review);
            unitWork.Save();
            return true;
        }

        public bool Update(int id, int patientId, CreateReviewDto reviewDto)
        {
            var review = unitWork.ReviewRepo.GetById(id);
            if (review == null || review.PatientId != patientId)
                return false;

            review.Rating = reviewDto.Rating;
            review.Comment = reviewDto.Comment;

            unitWork.ReviewRepo.Update(review);
            unitWork.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var review = unitWork.ReviewRepo.GetById(id);
            if (review == null)
                return false;

            unitWork.ReviewRepo.Delete(id);
            unitWork.Save();
            return true;
        }

        public double GetAverageRatingForDoctor(int doctorId)
        {
            var reviews = unitWork.ReviewRepo.GetAll()
                .Where(r => r.DoctorId == doctorId)
                .ToList();

            if (reviews.Count == 0)
                return 0;

            return reviews.Average(r => r.Rating);
        }
    }
}
