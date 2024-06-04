using Domain.Details;
using Domain.Products;

namespace Domain.Users;

public class Producer : User {
    public IEnumerable<Stock> Stocks { get; set; } = new List<Stock>();
    public IEnumerable<Review> ReviewsReceived { get; set; } = new List<Review>();

    public Producer() {
    }

    public Producer(int id, 
        string username, 
        string email, 
        string password, 
        string firstName, 
        string lastName, 
        DateOnly? dob, 
        Address? address, 
        IEnumerable<Review> reviewsReceived, 
        IEnumerable<Stock> stocks) : 
        base(id, username, email, password, firstName, lastName, dob, address, UserType.Producer) {
        ReviewsReceived = reviewsReceived;
        Stocks = stocks;
    }
}