using Domain.Users;

namespace WebApp.Models.DTO;

public class UserDto {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateOnly? Dob { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    public AddressDto? Address { get; set; }
}