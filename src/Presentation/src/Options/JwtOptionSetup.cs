using Microsoft.Extensions.Options;

using Words.Infastructure.Authentication;

namespace Words.Presentation.Options;

public class JwtOptionSetup : IConfigureOptions<JwtOptions> {
    private const string JwtSection = "Jwt";
    private readonly IConfiguration configuration;

  public JwtOptionSetup(IConfiguration configuration) {
    this.configuration = configuration;
  }

  public void Configure(JwtOptions options) {
      this.configuration.GetSection(JwtSection).Bind(options);
  }
}
