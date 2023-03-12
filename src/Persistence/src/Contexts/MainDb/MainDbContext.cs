using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Words.Domain.Entities;
using Words.Infastructure.Authentication;

namespace Words.Persistence.Contexts;

public class MainDbContext : DbContext {
  private readonly string connectionString;
  private readonly ILogger<MainDbContext> logger;

  public MainDbContext(ILogger<MainDbContext> logger) : base() {
    IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

    this.connectionString = configuration.GetConnectionString("MainDb");
    this.logger = logger;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    _ = optionsBuilder.UseMySql(this.connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
    _ = optionsBuilder.LogTo(message => logger.LogInformation(message), LogLevel.Information);
  }

  public DbSet<User> Users { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Word> Words { get; set; }
  public DbSet<Answer> Answers { get; set; }
}
