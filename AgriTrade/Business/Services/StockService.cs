using Domain.Products;
using Persistence.UnitOfWork;

namespace Business.Services;

public class StockService(IUnitOfWork unitOfWork) {
    public IEnumerable<Stock> GetAllStocks() {
        return unitOfWork.StockRepository.GetAll();
    }
}