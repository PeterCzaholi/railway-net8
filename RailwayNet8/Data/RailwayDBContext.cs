using Microsoft.EntityFrameworkCore;
using RailwayNet8.Data.DBModels;

namespace RailwayNet8.Data;

public class RailwayDBContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public RailwayDBContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConnectionHelper.GetConnectionString(_configuration);

        optionsBuilder.UseNpgsql(connectionString);
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}
