using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MineDyrAPI.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(
            connString,
            npgOptions => npgOptions.MigrationsHistoryTable("__EFMigrationsHistory", config["DefaultSchema"])  
        );

        return new AppDbContext(optionsBuilder.Options, new HttpContextAccessor());
    }
}