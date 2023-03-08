using Words.Domain.Repositories;
using Words.Persistence.Contexts;

namespace Words.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork {
  private readonly MainDbContext Context;

  public UnitOfWork(MainDbContext context) {
    Context = context;
  }

  public bool SaveChanges() {
    var result = this.Context.SaveChanges();

    return result > 0;
  }

  public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) {
    var result = await this.Context.SaveChangesAsync(cancellationToken);

    return result > 0;
  }
}
