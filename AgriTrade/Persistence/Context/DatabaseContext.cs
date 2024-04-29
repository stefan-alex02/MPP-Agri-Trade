using Domain.Details;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options), IDatabaseContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Quantity> Quantities { get; set; }

    public DatabaseContext() 
        : this(new DbContextOptionsBuilder<DatabaseContext>().Options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlite("Data Source=DB/AgriTrade.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Quantity>()
            .HasOne(q => q.Stock)
            .WithMany()
            .HasForeignKey(q => q.StockId);
        
        modelBuilder.Entity<Quantity>()
            .HasOne(q => q.Order)
            .WithMany(o => o.Products)
            .HasForeignKey(q => q.OrderId);
        
        modelBuilder.Entity<Quantity>()
            .HasIndex(q => new { q.StockId, q.OrderId })
            .IsUnique();
        
        
        modelBuilder.Entity<User>()
            .HasDiscriminator<UserType>("UserType")
            .HasValue<User>(UserType.NoType)
            .HasValue<Consumer>(UserType.Consumer)
            .HasValue<Producer>(UserType.Producer);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}