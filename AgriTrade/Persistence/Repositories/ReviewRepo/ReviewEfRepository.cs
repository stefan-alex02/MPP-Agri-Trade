using System.Linq.Expressions;
using Domain.Details;
using log4net;
using Persistence.Context;

namespace Persistence.Repositories.ReviewRepo;

public class ReviewEfRepository(IDatabaseContext context) : IReviewRepository, IDisposable {
    private static readonly ILog log = LogManager.GetLogger("ReviewEfRepository");

    public void Add(Review entity) {
        log.InfoFormat("Saving review {0}", entity.Id);
        context.Reviews.Add(entity);
        log.Info("Review saved");
    }

    public IEnumerable<Review> Find(Expression<Func<Review, bool>> predicate) {
        log.InfoFormat("Finding reviews by predicate {0}", predicate.ToString());
        return context.Reviews.Where(predicate).ToList();
    }

    public Review? Get(int id) {
        log.InfoFormat("Getting review by id {0}", id);
        return context.Reviews.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<Review> GetAll() {
        log.Info("Getting all reviews");
        return context.Reviews.ToList();
    }

    public void Remove(Review entity) {
        log.InfoFormat("Removing review {0}", entity.Id);
        context.Reviews.Remove(entity);
    }

    public void Dispose() {
        context.Dispose();
    }
}