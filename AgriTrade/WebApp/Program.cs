using Business.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.OrderRepo;
using Persistence.Repositories.StockRepo;
using Persistence.Repositories.UserRepo;
using Persistence.UnitOfWork;
using WebApp.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection")!
    .Replace("{Dir}", Directory.GetParent(Environment.CurrentDirectory)!.FullName);

builder.Services
    .AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

builder.Services
    .AddScoped<IDatabaseContext, DatabaseContext>()
    .AddScoped<IUserRepository, UserEfRepository>()
    .AddScoped<IOrderRepository, OrderEfRepository>()
    .AddScoped<IStockRepository, StockEfRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<UserService>()
    .AddScoped<StockService>();

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngularApp",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // Allow credentials
        });
});

builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(0.25);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

    // Add SameSite and Secure policy
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Only if using HTTPS
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<NotificationHub>("/notificationHub");

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();

app.Run();