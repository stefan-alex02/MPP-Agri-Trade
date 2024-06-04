using System.Linq.Expressions;
using Domain.Products;
using log4net;
using Persistence.Context;

namespace Persistence.Repositories.OrderRepo;

public class OrderEfRepository(IDatabaseContext context) : IOrderRepository, IDisposable {
    private static readonly ILog log = LogManager.GetLogger("OrderEfRepository");
    public void Add(Order entity) {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate) {
        throw new NotImplementedException();
    }

    public Order? Get(int id) {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll() {
        log.Info("Getting all orders");
        return context.Orders.ToList();
    }

    public void Remove(Order entity) {
        throw new NotImplementedException();
    }

    public void Dispose() {
        context.Dispose();
    }
}