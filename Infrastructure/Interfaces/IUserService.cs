using DoMain.Models;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Responce<List<User>>> GetUsersAsync();
    Task<Responce<User>> GetUserByIdAsync(int userId);
    Task<Responce<bool>> AddUserAsync(User user);
    Task<Responce<bool>> UpdateUserAsync(User user); 
    Task<Responce<bool>> DeleteUserAsync(int userId);
}