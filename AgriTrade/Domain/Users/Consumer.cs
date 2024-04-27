using Domain.Details;
using Domain.Products;

namespace Domain.Users;

public class Consumer : User {
    public IEnumerable<Order>? PastOrders { get; set; }
    public IEnumerable<Review>? ReviewsGiven { get; set; }
    
    public Consumer() {
    }
    
    public Consumer(int id, 
        string? username, 
        string email, 
        string? password, 
        string? firstName, 
        string? lastName, 
        DateOnly dob, 
        Address? address, 
        UserType userType, 
        IEnumerable<Review>? reviewsGiven, 
        IEnumerable<Order>? pastOrders) : 
        base(id, username, email, password, firstName, lastName, dob, address, userType) {
        ReviewsGiven = reviewsGiven;
        PastOrders = pastOrders;
    }
}