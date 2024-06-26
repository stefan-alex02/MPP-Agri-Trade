﻿using Domain.Details;
using Domain.Products;

namespace Domain.Users;

public class Consumer : User {
    public IEnumerable<Order> PastOrders { get; set; } = new List<Order>();
    public IEnumerable<Review> ReviewsGiven { get; set; } = new List<Review>();
    
    public Consumer() {
    }
    
    public Consumer(int id, 
        string username, 
        string email, 
        string password, 
        string firstName, 
        string lastName, 
        DateOnly? dob, 
        Address? address,
        IEnumerable<Review> reviewsGiven, 
        IEnumerable<Order> pastOrders) : 
        base(id, username, email, password, firstName, lastName, dob, address, UserType.Consumer) {
        ReviewsGiven = reviewsGiven;
        PastOrders = pastOrders;
    }
}