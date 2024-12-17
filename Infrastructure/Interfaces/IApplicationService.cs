using System.Net.Mime;
using DoMain.Models;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IApplicationService
{
    Task<Responce<List<Application>>> GetApplications();
    Task<Responce<Application>> GetApplicationByIdAsync(int id);
    Task<Responce<int>> GetCountApplicationByIdAsync();
    Task<Responce<Application>> GetApplicationByStatusAsync( string status);
    Task<Responce<bool>> AddApplicationAsync(Application application);
    Task<Responce<bool>> UpdateApplicationAsync(Application application);
    Task<Responce<bool>> DeleteApplicationAsync(int id);
}