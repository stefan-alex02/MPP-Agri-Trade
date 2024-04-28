using Domain.Details;
using Domain.Products;

namespace Domain.Users;

public class Producer : User {
    public IEnumerable<Stock>? Stocks { get; set; }
    public IEnumerable<Review>? ReviewsReceived { get; set; }

    public Producer() {
    }

    public Producer(int id, 
        string? username, 
        string email, 
        string? password, 
        string? firstName, 
        string? lastName, 
        DateOnly dob, 
        Address? address, 
        UserType userType, 
        IEnumerable<Review>? reviewsReceived, 
        IEnumerable<Stock>? stocks) : 
        base(id, username, email, password, firstName, lastName, dob, address, userType) {
        ReviewsReceived = reviewsReceived;
        Stocks = stocks;
    }
}