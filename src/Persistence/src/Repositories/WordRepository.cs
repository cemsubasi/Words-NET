using Microsoft.EntityFrameworkCore;

using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Persistence.Contexts;

namespace Words.Persistence.Repositories;

public class WordRepository : Repository<Word>, IWordRepository {
  private readonly MainDbContext mainDbContext;
  public WordRepository(MainDbContext mainDbContext) : base(mainDbContext) {
    this.mainDbContext = mainDbContext;
  }

  public async Task<List<Word>> GetWordsWithUserId(Guid id, CancellationToken cancellationToken) {
    return await this.mainDbContext.Words.Include(x => x.Category).Include(x => x.Answers).Where(x => x.Category.UserId == id).ToListAsync(cancellationToken);
  }
}
