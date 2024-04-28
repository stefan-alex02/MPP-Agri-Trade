using Domain.Details;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public interface IDatabaseContext : IDisposable {
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Quantities { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    public int SaveChanges();
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}