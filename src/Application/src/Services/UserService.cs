using Mapster;

using Words.Domain.Entities;
using Words.Domain.ValueObjects;
using Words.Domain.Repositories;
using Words.Application.Validations;

namespace Words.Application.Services;

public class UserService {
  private readonly IRepository<User> userRepository;
  private readonly IUnitOfWork unitOfWork;
  private readonly UserValidator userValidator;

  public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork, UserValidator userValidator) {
    this.userRepository = userRepository;
    this.unitOfWork = unitOfWork;
    this.userValidator = userValidator;
  }

  public async Task<bool> AddAsync(UserModel user, CancellationToken cancellationToken) {
    var validationResult = userValidator.Validate(user);
    if (!validationResult.IsValid) {
      return false;
    }

    var entity = User.Create(user.Username, user.Email, user.Password, null);
    this.userRepository.Add(entity);
    var result = await this.unitOfWork.SaveChangesAsync(cancellationToken);

    return result;
  }

  public async Task<List<UserModel>> GetAll() {
    var entity = await this.userRepository.GetAllAsync();
    var model = entity.Adapt<List<UserModel>>();

    return model;
  }
}
