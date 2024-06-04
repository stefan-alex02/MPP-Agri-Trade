using Domain.Details;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public interface IDatabaseContext : IDisposable {
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Review> Review { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    public int SaveChanges();
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}