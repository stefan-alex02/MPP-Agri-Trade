using Persistence.Context;
using Persistence.Repositories.OrderRepo;
using Persistence.Repositories.ReviewRepo;
using Persistence.Repositories.StockRepo;
using Persistence.Repositories.UserRepo;

namespace Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable {
    public IDatabaseContext DatabaseContext { get; }
    public IUserRepository UserRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IStockRepository StockRepository { get; }
    public IReviewRepository ReviewRepository { get; }
    
    public int SaveChanges();
    public Task SaveChangesAsync();
}