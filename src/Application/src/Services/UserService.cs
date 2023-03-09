using Mapster;

using Words.Domain.Entities;
using Words.Domain.ValueObjects;
using Words.Domain.Repositories;
using Words.Application.Validations;
using Words.Application.Helpers;
using FluentValidation;

namespace Words.Application.Services;

public class UserService {
  private readonly IRepository<User> userRepository;
  private readonly IUnitOfWork unitOfWork;
  private readonly IValidator<UserModel> userValidator;
  private readonly IPasswordManager passwordManager;

  public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork, IValidator<UserModel> userValidator, IPasswordManager passwordManager) {
    this.userRepository = userRepository;
    this.unitOfWork = unitOfWork;
    this.userValidator = userValidator;
    this.passwordManager = passwordManager;
  }

  public async Task<bool> AddAsync(UserModel user, CancellationToken cancellationToken) {
    var validationResult = userValidator.Validate(user);
    if (!validationResult.IsValid) {
      return false;
    }

    var passwordSalt = passwordManager.GenerateSalt(8);
    var passwordHash = passwordManager.GenerateHash(user.Password, passwordSalt);
    

    var entity = User.Create(user.Username, user.Email, passwordHash, passwordSalt, null);
    this.userRepository.Add(entity);
    var result = await this.unitOfWork.SaveChangesAsync(cancellationToken);

    return result;
  }

  public async Task<List<UserModel>> GetAll(CancellationToken cancellationToken) {
    var entity = await this.userRepository.GetAllAsync(cancellationToken);
    var model = entity.Adapt<List<UserModel>>();

    return model;
  }
}
