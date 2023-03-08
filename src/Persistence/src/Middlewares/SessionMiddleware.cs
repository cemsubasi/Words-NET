using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Words.Application.Services;
using Words.Persistence.Contexts;

namespace Words.Persistence.Middlewares;

public class SessionMiddleware : IMiddleware {
  private readonly MainDbContext dbContext;
  private readonly SessionService sessionService;
  private readonly ILogger<SessionMiddleware> logger;

  public SessionMiddleware(MainDbContext dbContext, SessionService sessionService, ILogger<SessionMiddleware> logger) {
    this.dbContext = dbContext;
    this.sessionService = sessionService;
    this.logger = logger;
  }

  public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
    logger.LogInformation(context.GetEndpoint()?.ToString());
    var claim = context.User.Claims.Where(x => x.Type == "id").FirstOrDefault();
      logger.LogCritical((claim is null).ToString());
    if (claim is not null) {
      var id = Guid.Parse(claim.Value);
      logger.LogCritical(claim.Value);
      var user = await this.dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
      if (user is not null) {
        sessionService.User = user;
      }
    }

    await next(context);
  }
}
