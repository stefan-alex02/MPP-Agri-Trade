using Business.Exceptions;
using Domain.Users;
using Persistence.UnitOfWork;

namespace Business.Services;

public class UserService(IUnitOfWork unitOfWork) {
    public User Login(string username, string password) {
        User? user = unitOfWork.UserRepository.GetByUsername(username);
        if (user is null || user.Password != password) {
            throw new AuthenticationException("Invalid username or password");
        }
        return user;
    }
}