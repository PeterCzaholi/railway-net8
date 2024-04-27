using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwayNet8.Data;
using RailwayNet8.Data.DBModels;

namespace RailwayNet8.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly RailwayDBContext _railwayDBContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, RailwayDBContext railwayDBContext)
    {
        _logger = logger;
        _railwayDBContext = railwayDBContext;
    }

    [HttpGet("/get-weather")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        await _railwayDBContext.Set<WeatherForecast>().AddRangeAsync(weather);

        await _railwayDBContext.SaveChangesAsync();

        _logger.LogInformation($"save data {DateTime.Now.ToString()}");

        return weather;
    }


    [HttpGet("/get-data")]
    public async Task<IEnumerable<WeatherForecast>> GetHistoricalData()
    {
        var aaa = await _railwayDBContext.Set<WeatherForecast>().ToListAsync();

        _logger.LogInformation($"get data {DateTime.Now.ToString()}");

        return aaa;
    }
}
