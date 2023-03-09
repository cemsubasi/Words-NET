using System.Text.Json;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Words.Application.Abstractions;
using Words.Application.Services;
using Words.Domain.ValueObjects;

namespace Words.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase {
  private readonly UserService userService;
  private readonly WordService wordService;
  private readonly AuthService authService;
  private readonly IJwtProvider jwtProvider;
  private readonly ILogger<AuthController> logger;

  public AuthController(UserService userService, AuthService authService, IJwtProvider jwtProvider, ILogger<AuthController> logger, WordService wordService) {
    this.userService = userService;
    this.authService = authService;
    this.jwtProvider = jwtProvider;
    this.logger = logger;
    this.wordService = wordService;
  }

  [HttpPost]
  public async Task<IActionResult> Signin([FromBody] SigninModel model, CancellationToken cancellationToken) {
    var result = await this.authService.Signin(model, cancellationToken);
    if (result is null) {
      var traceId = Guid.NewGuid();
      logger.LogError($"Unauthorized user credentials. TraceId: {traceId}");
      logger.LogError($"TraceId: {traceId}. RequestBody: {JsonSerializer.Serialize(model)}");

      HttpContext.Response.Headers.Authorization = string.Empty;
      return this.Unauthorized(new { Message = $"Unauthorized user credentials. TraceId: {traceId}" });
    }

    var token = jwtProvider.Generate(result);
    HttpContext.Response.Headers.Authorization = "Bearer " + token;

    var words = await this.wordService.GetWordsByCategory(result.Id, cancellationToken);

    return this.Ok(words);
  }
}
