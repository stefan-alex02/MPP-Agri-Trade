using Persistence.Context;
using Persistence.Repositories.OrderRepo;
using Persistence.Repositories.ReviewRepo;
using Persistence.Repositories.StockRepo;
using Persistence.Repositories.UserRepo;

namespace Persistence.UnitOfWork;

public class UnitOfWork(
    IDatabaseContext databaseContext, 
    IUserRepository userRepository,
    IOrderRepository orderRepository,
    IStockRepository stockRepository,
    IReviewRepository reviewRepository)
    : IUnitOfWork {
    
    public IDatabaseContext DatabaseContext { get; } = databaseContext;
    public IUserRepository UserRepository { get; } = userRepository;
    public IOrderRepository OrderRepository { get; } = orderRepository;
    public IStockRepository StockRepository { get; } = stockRepository;
    public IReviewRepository ReviewRepository { get; } = reviewRepository;

    public int SaveChanges() => DatabaseContext.SaveChanges();

    public async Task SaveChangesAsync() => await DatabaseContext.SaveChangesAsync();

    public void Dispose() => DatabaseContext?.Dispose();
}