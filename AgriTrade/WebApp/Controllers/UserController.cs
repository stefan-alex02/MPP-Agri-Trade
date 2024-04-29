using Business.Exceptions;
using Business.Services;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class UserController(UserService userService) : Controller {
    [HttpPost("api/user/login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest) {
        try {
            User user = userService.Login(loginRequest.Username, loginRequest.Password);
            
            HttpContext.Session.SetString("User_ID", user.Id.ToString());
            HttpContext.Session.SetString("User_Username", user.Username!);
            
            Console.WriteLine($"Session created for {user.Username} with ID {user.Id}");
            Console.WriteLine($"Session ID: {HttpContext.Session.Id}");
            
            LoginResponse loginResponse = new LoginResponse {
                UserType = user.UserType
            };
            
            return Ok(loginResponse);
        }
        catch (AuthenticationException e) {
            return StatusCode(450, e.Message);
        }
        catch (Exception e) {
            return BadRequest();
        }
    }
    
    [HttpPost("api/user/logout")]
    public async Task<ActionResult<HttpResponse>> Logout() {
        Console.WriteLine($"Session ID before clear: {HttpContext.Session.Id}");

        // Clear the session data on the server side
        HttpContext.Session.Clear();

        // Delete the session cookie
        HttpContext.Response.Cookies.Delete("ASP.NET_SessionId");
        
        Console.WriteLine($"Session ID after clear: {HttpContext.Session.Id}");

        return Ok();
    }
    
    [HttpGet("api/user/session")]
    public async Task<ActionResult> GetSession() {
        string? userId = HttpContext.Session.GetString("User_ID");
        string? username = HttpContext.Session.GetString("User_Username");
        
        if (userId == null || username == null) {
            return StatusCode(401, "No session found");
        }
        
        return Ok();
    }
}