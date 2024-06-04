using System.Linq.Expressions;
using Domain;
using Domain.Users;
using log4net;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.UserRepo;

public class UserEfRepository(IDatabaseContext context) : IUserRepository, IDisposable {
    private static readonly ILog log = LogManager.GetLogger("UserEfRepository");

    public void Add(User entity) {
        log.InfoFormat("Saving user {0}", entity.Username);
        context.Users.Add(entity);
        if (entity.Address is not null) {
            context.Addresses.Add(entity.Address);
        }
        log.Info("User saved");
    }

    public IEnumerable<User> Find(Expression<Func<User, bool>> predicate) {
        log.InfoFormat("Finding users by predicate {0}", predicate.ToString());
        return context.Users.Where(predicate).ToList();
    }

    public User? Get(int id) {
        log.InfoFormat("Getting user by id {0}", id);
        return context.Users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetAll() {
        log.Info("Getting all users");
        // var consumers = context.Users.OfType<Consumer>()
        //     .Include(c => c.PastOrders)
        //     .ToList();
        //
        // var otherUsers = context.Users.Where(u => !(u is Consumer)).ToList();
        //
        // return consumers.Cast<User>().Concat(otherUsers).ToList();
        return context.Users.ToList();
    }

    public void Remove(User entity) {
        throw new NotImplementedException();
    }

    public User? GetByUsername(string username) {
        log.InfoFormat("Getting user by username {0}", username);
        return context.Users.FirstOrDefault(u => u.Username == username);
    }

    public void Dispose() {
        context.Dispose();
    }
}