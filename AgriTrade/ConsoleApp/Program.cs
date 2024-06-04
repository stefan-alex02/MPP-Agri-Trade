// See https://aka.ms/new-console-template for more information

using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories.OrderRepo;
using Persistence.Repositories.ReviewRepo;
using Persistence.Repositories.StockRepo;
using Persistence.Repositories.UserRepo;
using Persistence.UnitOfWork;

var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
optionsBuilder.UseSqlite(ConfigurationManager
    .ConnectionStrings["DefaultConnection"]
    .ConnectionString.Replace("{Dir}", 
        Directory
            .GetParent(Environment.CurrentDirectory)!
            .Parent!
            .Parent!
            .Parent!
            .FullName));

IDatabaseContext dataBaseContext = new DatabaseContext(optionsBuilder.Options);
IUserRepository userRepository = new UserEfRepository(dataBaseContext);
IOrderRepository orderRepository = new OrderEfRepository(dataBaseContext);
IStockRepository stockRepository = new StockEfRepository(dataBaseContext);
IReviewRepository reviewRepository = new ReviewEfRepository(dataBaseContext);

IUnitOfWork unitOfWork = 
    new UnitOfWork(dataBaseContext, userRepository, orderRepository, stockRepository, reviewRepository);

var users = userRepository.GetAll();
// var orders = orderRepository.GetAll();

foreach (var user in users) {
    Console.WriteLine(user);
}

unitOfWork.SaveChanges();
