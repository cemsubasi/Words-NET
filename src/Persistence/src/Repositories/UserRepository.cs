using Microsoft.EntityFrameworkCore;

using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;
using Words.Persistence.Contexts;

namespace Words.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository {
  private readonly MainDbContext mainDbContext;
  public UserRepository(MainDbContext mainDbContext) : base(mainDbContext) {
    this.mainDbContext = mainDbContext;
  }

  public async Task<User?> GetUserWithCategory(Guid id) {
    return await this.mainDbContext.Users.Include(x => x.Categories).SingleOrDefaultAsync(x => x.Id == id);
  }

  public async Task<User?> VerifyUser(SigninModel model, CancellationToken cancellationToken) {
    Console.WriteLine(model.Username, model.Password);

    return await this.mainDbContext.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password, cancellationToken);
  }

  public async Task<User?> GetUserWithRelations(Guid id, CancellationToken cancellationToken) {
    return await this.mainDbContext.Users
      .Where(x => x.Id == id)
      .Include(x => x.Categories)
      .ThenInclude(x => x.Words)
      .ThenInclude(x => x.Answers)
      .SingleOrDefaultAsync(cancellationToken);
  }
}
