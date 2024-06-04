using Business.Exceptions;
using Business.Utils;
using Domain.Details;
using Domain.Products;
using Domain.Users;
using Persistence.UnitOfWork;

namespace Business.Services;

public class UserService(IUnitOfWork unitOfWork) {
    public User Login(string username, string password) {
        User? user = unitOfWork.UserRepository.GetByUsername(username);
        
        if (user is null || !PasswordService.VerifyPasswordHash(password, user.Password)) {
            throw new AuthenticationException("Invalid username or password");
        }
        
        return user;
    }
    
    public void Register(string username, string email, string password, 
        string firstName, string lastName, DateOnly? dob, UserType userType,
        int? addressNumber, string? addressStreet, string? addressCity, 
        string? addressCounty, int? addressZipCode) {
        if (unitOfWork.UserRepository.GetByUsername(username) is not null) {
            throw new ConflictException("Username already exists");
        }

        Address? address;
        if (addressNumber is not null && addressStreet is not null && addressCity is not null &&
            addressCounty is not null && addressZipCode is not null) {
            address = new Address(0, (int)addressNumber, addressStreet, addressCity,
                addressCounty, (int)addressZipCode, 0, 0);
        }
        else {
            address = null;
        }

        User user = userType switch {
            UserType.Consumer => new Consumer(0, username, email, PasswordService.HashPassword(password), 
                    firstName, lastName, dob, address, new List<Review>(), new List<Order>()),
            UserType.Producer => new Producer(0, username, email, PasswordService.HashPassword(password), 
                firstName, lastName, dob, address, new List<Review>(), new List<Stock>()),
            _ => throw new RegisterException("Invalid user type")
        };
        unitOfWork.UserRepository.Add(user);
        unitOfWork.SaveChanges();
    }
}