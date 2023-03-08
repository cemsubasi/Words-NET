using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Words.Application.Abstractions;
using Words.Domain.Entities;
using Words.Domain.ValueObjects;

namespace Words.Infastructure.Authentication;

public class JwtProvider : IJwtProvider {
  private readonly JwtOptions options;

  public JwtProvider(IOptions<JwtOptions> options) {
    this.options = options.Value;
  }

  public string Generate(User user) {
    var claims = new Claim[] {
      new Claim("id", user.Id.ToString()),
      /* new Claim(JwtRegisteredClaimNames.Name, user.Username.ToString()), */
      /* new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()), */
    };
    var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)), SecurityAlgorithms.HmacSha512);

    var token = new JwtSecurityToken(
      options.Issuer,
      options.Audience,
      claims,
      null,
      DateTime.UtcNow.AddDays(7),
      signinCredentials);
    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

    return tokenValue;
  }

  public static TokenValidationParameters GetValidationParameters(string[] args) {
    using IHost host = Host.CreateDefaultBuilder(args).Build();

    IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

    string audience = config["Jwt:Audience"];
    string issuer = config["Jwt:Issuer"];
    string secretKey = config["Jwt:SecretKey"];
    var key = Encoding.ASCII.GetBytes(secretKey);
    return new TokenValidationParameters() {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = issuer,
      ValidAudience = audience,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
  }
}
