using Domain.Details;
using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options), IDatabaseContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Quantities { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DatabaseContext() 
        : this(new DbContextOptionsBuilder<DatabaseContext>().Options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlite("Data Source=DB/AgriTrade.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>()
            .HasDiscriminator<UserType>("UserType")
            .HasValue<User>(UserType.NoType)
            .HasValue<Consumer>(UserType.Consumer)
            .HasValue<Producer>(UserType.Producer);

        /*
         migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderedById = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Users_OrderedById",
                        column: x => x.OrderedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
         */

        modelBuilder.Entity<Consumer>()
            .HasMany(c => c.PastOrders)
            .WithOne(o => o.OrderedBy);

        // modelBuilder.Entity<Order>()
        //     .HasOne(o => o.OrderedBy)
        //     .WithMany(c => c.PastOrders);
        // .HasForeignKey(o => o.OrderedById);

        // modelBuilder.Entity<Task>()
        //     .HasOne(a => a.AssignedTo)
        //     .WithMany(e => e.Tasks)
        //     .HasForeignKey(a => a.AssignedToId);   
        //
        // modelBuilder.Entity<Task>()
        //     .HasOne(a => a.CreatedBy)
        //     .WithMany(e => e.CreatedTasks)
        //     .HasForeignKey(a => a.CreatedById);  
    }
}