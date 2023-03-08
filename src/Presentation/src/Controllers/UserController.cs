using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Words.Application.Services;
using Words.Domain.ValueObjects;

namespace Words.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase {
  private readonly UserService userService;

  public UserController(UserService userService) {
    this.userService = userService;
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> CreateUser([FromBody] UserModel user, CancellationToken cancellationToken) {
    var result = await this.userService.AddAsync(user, cancellationToken);
    if (!result) {
      return this.UnprocessableEntity();
    }

    return this.NoContent();
  }

  [HttpGet]
  public async Task<IActionResult> GetUsers() {
    var result = await this.userService.GetAll();
    return this.Ok(result);
  }
}
