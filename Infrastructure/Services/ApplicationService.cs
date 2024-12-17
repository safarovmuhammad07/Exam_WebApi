using System.Net;
using Dapper;
using DoMain.Models;
using Infrastructure.ApiResponce;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ApplicationService(DapperContext context):IApplicationService
{
    public async Task<Responce<List<Application>>> GetApplications()
    {
        await using var connect = context.GetConnection();
        const string sql = "select * from Applications";
        var res = await connect.QueryAsync<Application>(sql);
        return new Responce<List<Application>>(res.ToList());
    }

    public async Task<Responce<Application>> GetApplicationByIdAsync(int id)
    {
        await using var connect = context.GetConnection();
        const string sql = "select * from Applications where Id = @Id";
        var res = await connect.QueryFirstOrDefaultAsync<Application>(sql, new { Id = id });
        return new Responce<Application>(res);
    }

    public async Task<Responce<int>> GetCountApplicationByIdAsync()
    {
        await using var connect = context.GetConnection();
        const string sql = "select Count(ApplicantId) from Applications";
        var res = await connect.QuerySingleAsync<int>(sql);
        return new Responce<int>(res);
    }

    public async Task<Responce<Application>> GetApplicationByStatusAsync(string status)
    {
        await using var connect = context.GetConnection();
        const string sql = "select * from Applications where Status = @Status";
        var res = await connect.QueryFirstOrDefaultAsync<Application>(sql, new { Status = status });
        return new Responce<Application>(res);
    }

    public async Task<Responce<bool>> AddApplicationAsync(Application application)
    {
        await using var connect = context.GetConnection();
        const string sql = "insert into Applications (JobId,ApplicantId,Resume,Status) values (@JobId,@ApplicantId,@Resume,@Status)";
        var res = await connect.ExecuteAsync(sql, application);
        return res == 0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.Created, "Application added");
    }

    public async Task<Responce<bool>> UpdateApplicationAsync(Application application)
    {
        await using var connect = context.GetConnection();
        const string sql = "Update Applications set JobId=@JobId,ApplicantId=@ApplicantId,Resume=@Resume,Status=@Status where Id = @Id ";
        var res = await connect.ExecuteAsync(sql, application);
        return res == 0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Internal Server Error") : new Responce<bool>(HttpStatusCode.OK, "Application updated");
    }

    public async Task<Responce<bool>> DeleteApplicationAsync(int id)
    {
        await using var connect = context.GetConnection();
        const string sql = "delete from Applications where Id = @Id";
        var res = await connect.ExecuteAsync(sql, new { Id = id });
        return res == 0 ? new Responce<bool>(HttpStatusCode.InternalServerError,"Application deleted") : new Responce<bool>(HttpStatusCode.OK,"Application deleted");
    }
}