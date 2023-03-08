using Microsoft.EntityFrameworkCore;

using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Persistence.Contexts;

namespace Words.Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository {
  private readonly MainDbContext context;
  public CategoryRepository(MainDbContext context) : base(context) {
    this.context = context;
  }

  public Task<List<Category>> GetAllWithNavigations(Guid userId, CancellationToken cancellationToken) {
    return this.context.Categories.Where(x => x.UserId == userId).Include(x => x.User).Include(x => x.Words).ThenInclude(x => x.Answers).ToListAsync(cancellationToken);
  }

  public Task<Category?> GetByIdWithNavigations(Guid id, CancellationToken cancellationToken) {
    return this.context.Categories.Where(x => x.Id == id).Include(x => x.User).Include(x => x.Words).ThenInclude(x => x.Answers).SingleOrDefaultAsync(cancellationToken);
  }
}
