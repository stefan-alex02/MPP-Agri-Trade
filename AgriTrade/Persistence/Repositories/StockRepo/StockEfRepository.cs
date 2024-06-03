using System.Linq.Expressions;
using Domain.Products;
using log4net;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.StockRepo;

public class StockEfRepository(IDatabaseContext context) : IStockRepository, IDisposable {
    private static readonly ILog log = LogManager.GetLogger("StockEfRepository");

    public void Add(Stock entity) {
        throw new NotImplementedException();
    }

    public IEnumerable<Stock> Find(Expression<Func<Stock, bool>> predicate) {
        throw new NotImplementedException();
    }

    public Stock? Get(int id) {
        log.InfoFormat("Getting stock by id {0}", id);
        return context.Stocks
            .Include(s => s.Producer)
            .Include(s => s.Product)
            .ThenInclude(p => p!.Category)
            .FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Stock> GetAll() {
        log.Info("Getting all stocks");
        return context.Stocks
            .Include(s => s.Producer)
            .Include(s => s.Product)
            .ThenInclude(p => p!.Category)
            .ToList();
    }

    public void Remove(Stock entity) {
        throw new NotImplementedException();
    }

    public Stock? GetByUsername(string username) {
        throw new NotImplementedException();
    }

    public void Dispose() {
        context.Dispose();
    }
}