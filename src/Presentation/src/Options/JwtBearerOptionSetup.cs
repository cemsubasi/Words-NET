using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Words.Infastructure.Authentication;

namespace Words.Presentation.Options;

public class JwtBearerOptionSetup : IConfigureOptions<JwtBearerOptions> {
  private readonly JwtOptions jwtOptions;

  public JwtBearerOptionSetup(IOptions<JwtOptions> jwtOptions) {
    this.jwtOptions = jwtOptions.Value;
  }

  public void Configure(JwtBearerOptions options) {
    options.TokenValidationParameters = new () {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtOptions.Issuer,
      ValidAudience = jwtOptions.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
    };
  }
}
