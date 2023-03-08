namespace Words.Domain.Repositories;

public interface IUnitOfWork {
  bool SaveChanges();
  Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
