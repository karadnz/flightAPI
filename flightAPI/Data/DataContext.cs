namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
//using WebApi.Entities;

//dotnet build
//dotnet ef migrations add NAME
//dotnet ef database update


using flightAPI;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Airport> Airports { get; set; }
    public DbSet<AircraftModel> AircraftModels { get; set; }
}