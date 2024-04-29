using System.Linq.Expressions;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.StockRepo;

public class StockEfRepository(IDatabaseContext context) : IStockRepository, IDisposable {
    public void Add(Stock entity) {
        throw new NotImplementedException();
    }

    public IEnumerable<Stock> Find(Expression<Func<Stock, bool>> predicate) {
        throw new NotImplementedException();
    }

    public Stock? Get(int id) {
        throw new NotImplementedException();
    }

    public IEnumerable<Stock> GetAll() {
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