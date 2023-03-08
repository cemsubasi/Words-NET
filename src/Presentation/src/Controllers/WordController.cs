using Microsoft.AspNetCore.Mvc;

using Words.Application.Services;
using Words.Domain.ValueObjects;

namespace Words.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordController : ControllerBase {
  private readonly WordService wordService;
  private readonly SessionService sessionService;

  public WordController(WordService wordService, SessionService sessionService) {
    this.wordService = wordService;
    this.sessionService = sessionService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateWord([FromBody] WordModel model, CancellationToken cancellationToken) {
    var result = await this.wordService.AddAsync(model, cancellationToken);
    if(!result) {
      return this.NotFound();
    }

    return this.Ok();
  }

  [HttpGet]
  public async Task<IActionResult> GetWords(CancellationToken cancellationToken) {
    /* var result = await this.wordService.GetWords(userId: id, cancellationToken); */
    var userId = this.sessionService.User.Id;
    var result = await this.wordService.GetWordsByCategory(userId, cancellationToken).ConfigureAwait(false);

    return this.Ok(result);
  }

  [HttpDelete]
  public async Task<IActionResult> RemoveWord([FromBody] WordModel model, CancellationToken cancellationToken) {
    var result = await this.wordService.AddAsync(model, cancellationToken);
    if(!result) {
      return this.NotFound();
    }

    return this.NoContent();
  }

  /* [HttpGet("")] */
}
