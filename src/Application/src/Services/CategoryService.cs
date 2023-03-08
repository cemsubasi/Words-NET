using Mapster;

using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;

namespace Words.Application.Services;

public class CategoryService {
  private readonly ICategoryRepository categoryRepository;
  private readonly IUserRepository userRepository;
  private readonly IUnitOfWork unitOfWork;
  private readonly SessionService sessionService;

  public CategoryService(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Word> wordRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, SessionService sessionService) {
    this.unitOfWork = unitOfWork;
    this.userRepository = userRepository;
    this.categoryRepository = categoryRepository;
    this.sessionService = sessionService;
  }

  public async Task<bool> AddAsync(CategoryModel category, CancellationToken cancellationToken) {
    var userId = this.sessionService.User.Id;

    Console.WriteLine($"category value: {category.Value}, userId: {userId}");
    var user = await userRepository.GetUserWithCategory(userId);
    if (user is null) {
      return false;
    }

    var isCategoryExist = user.Categories.Any(x => x.Value == category.Value);
    if (isCategoryExist) {
      return false;
    }

    var entity = Category.Create(category.Value, user);
    categoryRepository.Add(entity);
    var result = await this.unitOfWork.SaveChangesAsync(cancellationToken);

    return result;
  }

  public async Task<ICollection<CategoryResponseModel>> GetAllAsync() {
    var result = await this.categoryRepository.GetAllAsync();
    var model = result.Adapt<List<CategoryResponseModel>>();
    return model;
  }

  public async Task<List<CategoryWithNavigationsModel>> GetAllWithNavigations(CancellationToken cancellationToken) {
    var userId = this.sessionService.User.Id;
    var result = await this.categoryRepository.GetAllWithNavigations(userId, cancellationToken);
    var model = result.Adapt<List<CategoryWithNavigationsModel>>();

    return model;
  }

  public async Task<bool> RemoveCategory(Guid id, CancellationToken cancellationToken) {
    var category = await this.categoryRepository.GetById(id);
    if (category is not null) {
      this.categoryRepository.Remove(category);
    }

    var result = await this.unitOfWork.SaveChangesAsync(cancellationToken);

    return result;
  }
}
