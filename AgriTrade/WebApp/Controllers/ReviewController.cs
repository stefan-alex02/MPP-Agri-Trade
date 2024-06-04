using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DTO;

namespace WebApp.Controllers;

public class ReviewController(ReviewService reviewService) : Controller {
    [HttpGet("api/reviews")]
    [Authorize]
    public ActionResult<ReviewDto[]> GetReviews() {
        if (HttpContext.User.Identity is not { IsAuthenticated: true }) {
            return Unauthorized();
        }
        
        try {
            IEnumerable<ReviewDto> reviews = reviewService.GetAllReviews().Select(r => new ReviewDto(
                r.Id, r.From.FirstName+" "+r.From.LastName,r.From.Username, r.To.FirstName+" "+r.To.LastName,r.To.Username,r.Description,r.Rating,r.Date.ToString()
            ));
            
            return Ok(reviews.ToArray());
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("api/reviews/top5")]
    [Authorize]
    public ActionResult<ReviewDto[]> GetTop5Reviews() {
        if (HttpContext.User.Identity is not { IsAuthenticated: true }) {
            return Unauthorized();
        }
        
        try {
            IEnumerable<ReviewDto> reviews = reviewService.GetTop5Reviews().Select(r => new ReviewDto(
                r.Id, r.From!.FirstName!+" "+r.From!.LastName!,r.From!.Username!, r.To!.FirstName!+" "+r.To!.LastName!,r.To!.Username!,r.Description,r.Rating,r.Date.ToString()
            ));
            
            return Ok(reviews.ToArray());
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}