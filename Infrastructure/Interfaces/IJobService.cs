using DoMain.Models;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IJobService
{
    Task<Responce<List<Job>>> GetJobsAsync();
    Task<Responce<List<Job>>> GeTlastJobsAsync();
    Task<Responce<Job>> GetJobByIdAsync(int jobId);
    
    Task<Responce<Job>> GetMaxSalaryJobByIdAsync();
    Task<Responce<Job>> GetMinSalaryJobByIdAsync();
    
    Task<Responce<int>> GetJobAvarageAsync();
    Task<Responce<bool>> AddJobAsync(Job job);
    Task<Responce<bool>> UpdateJobAsync(Job job); 
    Task<Responce<bool>> DeleteJobAsync(Job job);
}