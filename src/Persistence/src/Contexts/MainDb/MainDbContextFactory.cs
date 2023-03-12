using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Words.Infastructure.Authentication;

namespace Words.Persistence.Contexts;

/* public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext> { */
/* private readonly ILogger<MainDbContext> logger; */

/* public MainDbContextFactory(ILogger<MainDbContext> logger) { */
/*   this.logger = logger; */
/* } */

/* public MainDbContext CreateDbContext(params string[] args) { */
/*   IConfigurationRoot configuration = new ConfigurationBuilder() */
/*     .SetBasePath(Directory.GetCurrentDirectory()) */
/*     .AddJsonFile("appsettings.json") */
/*     .Build(); */

/*   var connectionString = configuration.GetConnectionString("MainDb"); */

/*   var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>(); */
/*   _ = optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))); */
/*   /1* _ = optionsBuilder.LogTo(message => this.logger.LogInformation(message)); *1/ */

/*   return new MainDbContext(optionsBuilder.Options); */
/* } */
/* } */
