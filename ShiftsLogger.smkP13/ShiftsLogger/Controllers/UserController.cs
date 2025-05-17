using Microsoft.AspNetCore.Mvc;
using ShiftWebApi.Models;
using ShiftWebApi.Services;

namespace ShiftWebApi.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
        return Ok(_userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        User? user = _userService.GetUserById(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        return Ok(_userService.CreateUser(user));
    }

    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(int id,User user)
    {
        User result = _userService.UpdateUser(id, user);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string?> DeleteUser(int id)
    {
        string? result = _userService.DeleteUser(id);
        return result == null ? NotFound() : Ok(result);
    }
}
