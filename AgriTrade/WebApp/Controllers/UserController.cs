using System.Security.Authentication;
using Business.Services;
using Domain;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class UserController(UserService userService) : Controller {
    private readonly UserService _userService = userService;

    [HttpPost("api/user")]
    public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel) {
        try {
            User user = _userService.(loginModel.Username, loginModel.Password);
            return Ok(user);
        }
        catch (AuthenticationException e) {
            return StatusCode(450, e.Message);
        }
        catch (Exception e) {
            return BadRequest();
        }
    }
}