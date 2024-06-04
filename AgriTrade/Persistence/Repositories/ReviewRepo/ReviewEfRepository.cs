using System.Linq.Expressions;
using Domain.Details;
using log4net;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.ReviewRepo;

public class ReviewEfRepository(IDatabaseContext context) : IReviewRepository, IDisposable {
    private static readonly ILog log = LogManager.GetLogger("ReviewEfRepository");

    public void Add(Review entity) {
        log.InfoFormat("Saving review {0}", entity.Id);
        context.Review.Add(entity);
        log.Info("Review saved");
    }

    public IEnumerable<Review> Find(Expression<Func<Review, bool>> predicate) {
        log.InfoFormat("Finding reviews by predicate {0}", predicate.ToString());
        return context.Review.Where(predicate)
            .Include(r=>r.From)
            .Include(r=>r.To)
            .ToList();
    }

    public Review? Get(int id) {
        log.InfoFormat("Getting review by id {0}", id);
        return context.Review
            .Include(r=>r.From)
            .Include(r=>r.To)
            .FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<Review> GetAll() {
        log.Info("Getting all reviews");
        return context.Review
            .Include(r=>r.From)
            .Include(r=>r.To)
            .ToList();
    }

    public void Remove(Review entity) {
        log.InfoFormat("Removing review {0}", entity.Id);
        context.Review.Remove(entity);
    }

    public void Dispose() {
        context.Dispose();
    }
}