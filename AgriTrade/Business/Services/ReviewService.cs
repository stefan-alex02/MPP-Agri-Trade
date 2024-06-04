using Domain.Details;
using Persistence.UnitOfWork;

namespace Business.Services;

public class ReviewService(IUnitOfWork unitOfWork) {
    public IEnumerable<Review> GetAllReviews() {
        return unitOfWork.ReviewRepository.GetAll();
    }
    public IEnumerable<Review> GetTop5Reviews() {
        IEnumerable<Review> allReviews = GetAllReviews();
        return allReviews.OrderByDescending(r => r.Rating).Take(5);
    }
}