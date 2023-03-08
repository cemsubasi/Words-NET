using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;

namespace Words.Application.Services;

public class AuthService {
  private readonly IUserRepository userRepository;

  public AuthService(IUserRepository userRepository) {
    this.userRepository = userRepository;
  }

  public async Task<User?> Signin(SigninModel model, CancellationToken cancellationToken) {
    return await this.userRepository.VerifyUser(model, cancellationToken); 
  }
}
