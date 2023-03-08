using Words.Domain.Entities;
using Words.Domain.ValueObjects;

namespace Words.Domain.Repositories;

public interface ICategoryRepository: IRepository<Category> {
  Task<List<Category>> GetAllWithNavigations(Guid userId, CancellationToken cancellationToken);
  Task<Category?> GetByIdWithNavigations(Guid id, CancellationToken cancellationToken);
}
