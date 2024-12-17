using DoMain.Models;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class JobController(IJobService jobService) : ControllerBase
{
    [HttpGet]
    public Task<Responce<List<Job>>> Get()
    {
        return jobService.GetJobsAsync();
    }

    [HttpGet("{id}")]
    public Task<Responce<Job>> Get(int id)
    {
        return jobService.GetJobByIdAsync(id);
    }

    [HttpGet("job/{LastJob}")]
    public Task<Responce<List<Job>>> GetLastJobs(int lastJob)
    {
        return jobService.GeTlastJobsAsync();
    }

    [HttpGet("job/{MaxSalary}")]
    public Task<Responce<Job>> GetJobsMAxSalary()
    {
        return jobService.GetMaxSalaryJobByIdAsync();
    }

    [HttpGet("job/{MinSalary}/{Salary}")]
    public Task<Responce<Job>> GetJobs()
    {
        return jobService.GetMinSalaryJobByIdAsync();
    }

    [HttpGet("job/{Avarage}")]
    public Task<Responce<int>> GetJobs(int avarage)
    {
        return jobService.GetJobAvarageAsync();
    }

    [HttpPost]
    public Task<Responce<bool>> Post([FromBody] Job job)
    {
        return jobService.AddJobAsync(job);
    }

    [HttpPut]
    public Task<Responce<bool>> Put([FromBody] Job job)
    {
        return jobService.UpdateJobAsync(job);
    }

    [HttpDelete]
    public Task<Responce<bool>> Delete(Job job)
    {
        return jobService.DeleteJobAsync(job);
    }
}