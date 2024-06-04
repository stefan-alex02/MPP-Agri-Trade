using Business.Exceptions;
using Business.Services;
using Domain.Users;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Authentication;
using WebApp.Models;

namespace WebApp.Controllers;

public class UserController(UserService userService, JwtService jwtService) : Controller {
    private static readonly ILog log = LogManager.GetLogger("UserController");
    
    [HttpPost("api/users/login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest loginRequest) {
        try {
            User user = userService.Login(loginRequest.Username, loginRequest.Password);
            var token = jwtService.GenerateToken(user.Id, user.Username, 
                user.FirstName + " " + user.LastName , user.UserType);
            
            Console.WriteLine($"User {user.Username} logged in with ID {user.Id}");
            
            Response.Headers["Authorization"] = $"Bearer {token}";
            LoginResponse loginResponse = new LoginResponse(token);
            
            return Ok(loginResponse);
        }
        catch (AuthenticationException e) {
            log.ErrorFormat("Failed to login user {0}", loginRequest.Username);
            return NotFound(e.Message);
        }
        catch (Exception e) {
            log.Error("Failed to login user", e);
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
    
    [HttpPost("api/users/register")]
    public ActionResult<HttpResponse> Register([FromBody] RegisterRequest registerRequest) {
        try {
            if (registerRequest.User.Address is null) {
                userService.Register(registerRequest.User.Username, registerRequest.User.Email, 
                    registerRequest.User.Password, registerRequest.User.FirstName, 
                    registerRequest.User.LastName, registerRequest.User.Dob, 
                    registerRequest.User.UserType, null, null, null, null, null);
            }
            else {
                userService.Register(registerRequest.User.Username, registerRequest.User.Email, 
                    registerRequest.User.Password, registerRequest.User.FirstName, 
                    registerRequest.User.LastName, registerRequest.User.Dob, 
                    registerRequest.User.UserType, registerRequest.User.Address.Number, 
                    registerRequest.User.Address.Street, registerRequest.User.Address.City, 
                    registerRequest.User.Address.County, registerRequest.User.Address.ZipCode);
            }
            
            Console.WriteLine($"User {registerRequest.User.Username} registered");
            
            return Ok();
        }
        catch(ConflictException e) {
            log.ErrorFormat("Failed to register user {0} StackTrace: {1}", 
                registerRequest.User.Username, e.StackTrace);
            return StatusCode(409, e.Message);
        }
        catch (RegisterException e) {
            log.ErrorFormat("Failed to register user {0} StackTrace: {1}", 
                registerRequest.User.Username, e.StackTrace);
            return BadRequest(e.Message);
        }
        catch (Exception e) {
            log.Error("Failed to register user", e);
            return BadRequest();
        }
    }
}