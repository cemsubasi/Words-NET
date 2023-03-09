using Words.Application.Helpers;
using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;

namespace Words.Application.Services;

public class AuthService {
  private readonly IUserRepository userRepository;
  private readonly IPasswordManager passwordManager;

  public AuthService(IUserRepository userRepository, IPasswordManager passwordManager) {
    this.userRepository = userRepository;
    this.passwordManager = passwordManager;
  }

  public async Task<User?> Signin(SigninModel model, CancellationToken cancellationToken) {
    var user = await this.userRepository.SingleOrDefaultAsync(x => x.Username == model.Username, cancellationToken).ConfigureAwait(false);
    if (user is null) {
      return null;
    }

    var passwordHash = passwordManager.GenerateHash(model.Password, user.PasswordSalt);
    if (user.Password != passwordHash) {
      return null;
    }

    return user;
  }
}
