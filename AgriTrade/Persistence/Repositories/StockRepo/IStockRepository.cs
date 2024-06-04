using Domain.Products;
using Domain.Users;

namespace Persistence.Repositories.StockRepo;

public interface IStockRepository : IRepository<Stock, int> {
}