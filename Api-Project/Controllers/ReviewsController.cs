using Api_Project.DTOs.Review;
using Api_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService reviewService;

        public ReviewsController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public ActionResult GetReviews()
        {
            try
            {
                var reviews = reviewService.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving reviews", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult GetReview(int id)
        {
            try
            {
                var review = reviewService.GetReviewById(id);
                if (review == null)
                    return NotFound(new { message = "Review not found" });

                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving review", error = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId:int}")]
        [AllowAnonymous]
        public ActionResult GetReviewsByDoctor(int doctorId)
        {
            try
            {
                var reviews = reviewService.GetReviewsByDoctor(doctorId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving doctor reviews", error = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId:int}/average-rating")]
        [AllowAnonymous]
        public ActionResult GetAverageRating(int doctorId)
        {
            try
            {
                var averageRating = reviewService.GetAverageRatingForDoctor(doctorId);
                return Ok(new { doctorId, averageRating });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving average rating", error = ex.Message });
            }
        }

        [HttpGet("patient/{patientId:int}")]
        public ActionResult GetReviewsByPatient(int patientId)
        {
            try
            {
                var reviews = reviewService.GetReviewsByPatient(patientId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving patient reviews", error = ex.Message });
            }
        }

        [HttpPost("patient/{patientId:int}")]
        public ActionResult Create(int patientId, CreateReviewDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = reviewService.Create(patientId, dto);
                if (!result)
                    return BadRequest(new { message = "Failed to create review. Appointment may not exist or review already exists." });

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating review", error = ex.Message });
            }
        }

        [HttpPut("{id:int}/patient/{patientId:int}")]
        public ActionResult Update(int id, int patientId, CreateReviewDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { message = "Data is null" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = reviewService.Update(id, patientId, dto);
                if (!result)
                    return NotFound(new { message = "Review not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating review", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = reviewService.Delete(id);
                if (!result)
                    return NotFound(new { message = "Review not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting review", error = ex.Message });
            }
        }
    }
}
