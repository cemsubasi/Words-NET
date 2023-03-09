using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Words.Application.Services;
using Words.Domain.ValueObjects;

namespace Words.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase {
  private readonly SessionService sessionService;
  private readonly CategoryService categoryService;

  public CategoryController(CategoryService categoryService, SessionService sessionService) {
    this.categoryService = categoryService;
    this.sessionService = sessionService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateCategory([FromBody] CategoryModel model, CancellationToken cancellationToken) {
     Console.WriteLine("CreateCategory " + this.sessionService.User?.Id);
    var result = await this.categoryService.AddAsync(model, cancellationToken);
    if (!result) {
      return this.UnprocessableEntity();
    }

    return this.NoContent();
  }

  [HttpGet]
  public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
    var result = await this.categoryService.GetAllAsync(cancellationToken);
    return this.Ok(result);
  }

  [HttpGet("navigation")]
  public async Task<IActionResult> GetAllWithNavigations(CancellationToken cancellationToken) {
    var result = await this.categoryService.GetAllWithNavigations(cancellationToken);
    return this.Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> RemoveCategory([FromRoute] Guid id, CancellationToken cancellationToken) {
    var result = await this.categoryService.RemoveCategory(id, cancellationToken).ConfigureAwait(false);
    return this.Ok(result);
  }
}
