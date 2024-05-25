using Business.Exceptions;
using Business.Services;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers;

public class UserController(UserService userService, JwtService jwtService) : Controller {
    [HttpPost("api/users/login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest loginRequest) {
        try {
            User user = userService.Login(loginRequest.Username, loginRequest.Password);
            var token = jwtService.GenerateToken(user.Id, user.Username, user.UserType);
            
            Console.WriteLine($"User {user.Username} logged in with ID {user.Id}");
            
            Response.Headers["Authorization"] = $"Bearer {token}";
            LoginResponse loginResponse = new LoginResponse(token);
            
            return Ok(loginResponse);
        }
        catch (AuthenticationException e) {
            return StatusCode(450, e.Message);
        }
        catch (Exception e) {
            return BadRequest();
        }
    }
    
    [HttpPost("api/users/logout")]
    [Authorize]
    public ActionResult<HttpResponse> Logout() {
        if (HttpContext.User.Identity is not { IsAuthenticated: true }) {
            return Unauthorized();
        }
        
        Console.WriteLine("User logging out");

        return Ok();
    }
}