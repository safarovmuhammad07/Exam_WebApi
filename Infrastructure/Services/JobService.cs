using System.Net;
using Dapper;
using DoMain.Models;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class JobService(DapperContext context):IJobService
{
    public async Task<Responce<List<Job>>> GetJobsAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = @"select * from Jobs";
        var res = connect.QueryAsync<Job>(sql);
        return new Responce<List<Job>>(res.Result.ToList());
    }

    public async Task<Responce<List<Job>>> GeTlastJobsAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = @"select * from Jobs order by CreatedAt desc limit 10";
        var res = await connect.QueryAsync<Job>(sql);
        return new Responce<List<Job>>(res.ToList());
    }

    public async Task<Responce<Job>> GetJobByIdAsync(int jobId)
    {
        await using var connect = context.GetConnection();
        const string sql = @"select * from Jobs where Id = @Id";
        var res =await connect.QueryAsync<Job>(sql, new { Id = jobId });
        return new Responce<Job>(res.FirstOrDefault());
    }

    public async Task<Responce<Job>> GetMaxSalaryJobByIdAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = @"select * from Jobs order by Salary desc limit 1";
        var res = await connect.QuerySingleAsync<Job>(sql);
        return new Responce<Job>(res);
        
    }

    public async Task<Responce<Job>> GetMinSalaryJobByIdAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = @"select * from Jobs order by Salary limit 1";
        var res = await connect.QuerySingleAsync<Job>(sql);
        return new Responce<Job>(res);
        
    }

    public async Task<Responce<int>> GetJobAvarageAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = @"select avg(Salary) from Jobs";
        var res = await connect.QuerySingleAsync<int>(sql);
        return new Responce<int>(res);
    }

    public async Task<Responce<bool>> AddJobAsync(Job job)
    {
        await using var connect = context.GetConnection();
        const string sql ="insert into Jobs (EmployeeId,Title,Description,Country,City,Status,CreatedAt,UpdatedAt) values (@EmployeeId,@Title,@Description,@Country,@City,@Status,@CreatedAt,@UpdatedAt)";
        var res = await connect.ExecuteAsync(sql, job);
        return res == 0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.Created, "Added");
        
    }

    public async Task<Responce<bool>> UpdateJobAsync(Job job)
    {
        await using var connect = context.GetConnection();
        const string sql="Update Jobs set EmployeeId=@EmployeeId,Title=@Title,Description=@Description,Country=@Country,City=@City,Status=@Status,CreatedAt=current_time,UpdatedAt=current_time where Id=@Id";
        var res = await connect.ExecuteAsync(sql, job);
        return res==0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.OK, "Updated");
    }

    public async Task<Responce<bool>> DeleteJobAsync(Job job)
    {
        await using var connect = context.GetConnection();
        const string sql = "Delete from Jobs where Id = @Id";
        var res = await connect.ExecuteAsync(sql, job);
        return res==0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.OK,"Deleted");
    }
}