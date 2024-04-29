using System.Linq.Expressions;
using Domain.Products;
using Persistence.Context;

namespace Persistence.Repositories.OrderRepo;

public class OrderEfRepository(IDatabaseContext context) : IOrderRepository, IDisposable {
    public void Add(Order entity) {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate) {
        throw new NotImplementedException();
    }

    public Order? Get(int id) {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll() => context.Orders.ToList();

    public void Remove(Order entity) {
        throw new NotImplementedException();
    }

    public void Dispose() {
        context.Dispose();
    }
}