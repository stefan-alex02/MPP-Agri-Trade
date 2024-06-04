using System.ComponentModel.DataAnnotations;
using Domain.Details;

namespace Domain.Users;

public class User : Entity<int> {
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public DateOnly? Dob { get; set; }
    public Address? Address { get; set; }
    public UserType UserType { get; set; }
    
    public User() : base(default) {
    }
    
    public User(int id, string username, string email, 
            string password, string firstName, string lastName, DateOnly? dob, 
            Address? address, UserType userType) : 
        base(id) {
        Username = username;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Dob = dob;
        Address = address;
        UserType = userType;
    }

    public override string ToString() {
        return $"User: {FirstName} {LastName} ({Username} - {Email})";
    }
}