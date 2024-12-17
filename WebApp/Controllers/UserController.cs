using DoMain.Models;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService):ControllerBase
{
    [HttpGet]
    public Task<Responce<List<User>>> Get()
    {
        return userService.GetUsersAsync();
    }

    [HttpGet("{id}")]
    public Task<Responce<User>> Get(int id)
    {
        return userService.GetUserByIdAsync(id);
    }

    [HttpPost]
    public Task<Responce<bool>> Post(User user)
    {
        return userService.AddUserAsync(user);
    }

    [HttpPut]
    public Task<Responce<bool>> Put(User user)
    {
        return userService.UpdateUserAsync(user);
    }

    [HttpDelete]
    public Task<Responce<bool>> Delete(int id)
    {
        return userService.DeleteUserAsync(id);
    }
}