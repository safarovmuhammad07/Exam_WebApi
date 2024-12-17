using System.Net;
using Dapper;
using DoMain.Models;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UserService(DapperContext context):IUserService
{
    public async Task<Responce<List<User>>> GetUsersAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = "select * from users";
        var res = connect.QueryAsync<User>(sql);
        return new Responce<List<User>>(res.Result.ToList());
    }

    public async Task<Responce<User>> GetUserByIdAsync(int userId)
    {
        await using var connect = context.GetConnection();
        const string sql = "select * from users where id = @id";
        var res = connect.QueryAsync<User>(sql, new { id = userId });
        return new Responce<User>(res.Result.FirstOrDefault());
    }

    public async Task<Responce<bool>> AddUserAsync(User user)
    {
        await using var connect = context.GetConnection();
        const string sql = "Insert into users (FullName,Email,Phone,Role,CreatedAt) values (@FullName,@Email,@Phone,@Role,@CreatedAt);";
        var res = await connect.ExecuteAsync(sql, user);
        if(res == 0)return new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error");
        return new Responce<bool>(HttpStatusCode.Created,"User added successfully");    
    }

    public async Task<Responce<bool>> UpdateUserAsync(User user)
    {
        await using var connect = context.GetConnection();
        const string sql =
            "Update Users set FullName=@FullName,Email=@Email,Phone=@Phone,Role=@Role,CreatedAt=current_time where id = @id;";
        var res = await connect.ExecuteAsync(sql, user);
        return res == 0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.OK,"User updated successfully");
    }

    public async Task<Responce<bool>> DeleteUserAsync(int userId)
    {
        await using var connect = context.GetConnection();
        const string sql = "Delete from users where id = @id";
        var res = await connect.ExecuteAsync(sql, new { id = userId });
        return res == 0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.OK,"User deleted successfully");
    }
}