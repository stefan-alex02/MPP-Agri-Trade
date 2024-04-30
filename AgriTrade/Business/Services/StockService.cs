using Domain.Products;
using Persistence.UnitOfWork;

namespace Business.Services;

public class StockService(IUnitOfWork unitOfWork) {
    public IEnumerable<Stock> GetAllStocks() {
        return unitOfWork.StockRepository.GetAll();
    }
    
    public Stock GetStockById(int id) {
        return unitOfWork.StockRepository.Get(id) ?? 
               throw new Exception("Stock not found");
    }
}