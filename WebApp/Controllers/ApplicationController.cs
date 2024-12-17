using DoMain.Models;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class ApplicationController(IApplicationService applicationService):ControllerBase
{
    [HttpGet]
    public Task<Responce<List<Application>>> Get()
    {
        return applicationService.GetApplications();
    }

    [HttpGet("{id}")]
    public Task<Responce<Application>> Get(int id)
    {
        return applicationService.GetApplicationByIdAsync(id);
    }

    [HttpGet("status")]
    public Task<Responce<Application>> GetStatus(string status)
    {
        return applicationService.GetApplicationByStatusAsync(status);
    }

    [HttpGet("Count")]
    public Task<Responce<int>> GetCount()
    {
        return applicationService.GetCountApplicationByIdAsync();
    }

    [HttpPost]
    public Task<Responce<bool>> Post(Application application)
    {
        return applicationService.AddApplicationAsync(application);
    }

    [HttpPut]
    public Task<Responce<bool>> Put(Application application)
    {
        return applicationService.UpdateApplicationAsync(application);
    }

    [HttpDelete]
    public Task<Responce<bool>> Delete(int id)
    {
        return applicationService.DeleteApplicationAsync(id);
    }
}