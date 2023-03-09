using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Words.Infastructure.Authentication;

namespace Words.Persistence.Contexts;

public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext> {
  public MainDbContext CreateDbContext(params string[] args) {
    IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

    var ConnectionString = configuration.GetConnectionString("MainDb");

    var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
    _ = optionsBuilder.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 32)));

    return new MainDbContext(optionsBuilder.Options);
  }
}
