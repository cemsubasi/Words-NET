using Microsoft.Extensions.Options;

using Words.Infastructure.Authentication;

namespace Words.Presentation.Options;

public class DbOptionSetup : IConfigureOptions<DbOptions> {
    private const string DbSection = "MainDb";
    private readonly IConfiguration configuration;

  public DbOptionSetup(IConfiguration configuration) {
    this.configuration = configuration;
  }

  public void Configure(DbOptions options) {
      this.configuration.GetSection(DbSection).Bind(options);
  }
}
