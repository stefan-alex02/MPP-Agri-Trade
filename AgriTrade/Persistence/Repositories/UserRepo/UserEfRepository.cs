using System.Linq.Expressions;
using Domain;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.UserRepo;

public class UserEfRepository(IDatabaseContext context) : IUserRepository, IDisposable {
    public void Add(User entity) {
        context.Users.Add(entity);
    }

    public IEnumerable<User> Find(Expression<Func<User, bool>> predicate) {
        return context.Users.Where(predicate).ToList();
    }

    public User? Get(int id) {
        return context.Users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetAll() {
        var consumers = context.Users.OfType<Consumer>()
            .Include(c => c.PastOrders)
            .ToList();

        var otherUsers = context.Users.Where(u => !(u is Consumer)).ToList();

        return consumers.Cast<User>().Concat(otherUsers).ToList();
    }

    public void Remove(User entity) {
        throw new NotImplementedException();
    }

    public User? GetByUsername(string username) {
        return context.Users.FirstOrDefault(u => u.Username == username);
    }

    public void Dispose() {
        context.Dispose();
    }
}