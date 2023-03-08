using Words.Domain.Entities;
using Words.Domain.ValueObjects;

namespace Words.Domain.Repositories;

public interface IUserRepository: IRepository<User> {
  Task<User?> GetUserWithCategory(Guid id);
  Task<User?> VerifyUser(SigninModel model, CancellationToken cancellationToken);
  Task<User?> GetUserWithRelations(Guid id, CancellationToken cancellationToken);
}
