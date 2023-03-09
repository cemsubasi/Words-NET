using Mapster;

using Words.Domain.Entities;
using Words.Domain.Repositories;
using Words.Domain.ValueObjects;

namespace Words.Application.Services;

public class WordService {
  private readonly IWordRepository wordRepository;
  private readonly IUserRepository userRepository;
  private readonly IRepository<Answer> answerRepository;
  private readonly ICategoryRepository categoryRepository;
  private readonly IUnitOfWork unitOfWork;

  public WordService(IUnitOfWork unitOfWork, IWordRepository wordRepository, ICategoryRepository categoryRepository, IRepository<Answer> answerRepository, IUserRepository userRepository) {
    this.unitOfWork = unitOfWork;
    this.wordRepository = wordRepository;
    this.categoryRepository = categoryRepository;
    this.answerRepository = answerRepository;
    this.userRepository = userRepository;
  }

  public async Task<bool> AddAsync(WordModel model, CancellationToken cancellationToken) {
    if (model is null) {
      return false;
    }

    /* var category = categoryRepository.Where(x => x.Id == model.CategoryId && x.UserId == model.UserId).SingleOrDefault(); */
    var category = await categoryRepository.GetByIdWithNavigations(model.CategoryId, cancellationToken);
    if (category is null) {
      return false;
    }

    var entity = Word.Create(model.Question, category);

    var answers = model.Answers.Select(x => Answer.Create(x.Value, entity)).ToList();

    wordRepository.Add(entity);
    await answerRepository.AddRangeAsync(answers, cancellationToken);

    var result = await this.unitOfWork.SaveChangesAsync(cancellationToken);

    return result;
  }
  public async Task<IEnumerable<WordModel>> GetWords(Guid userId, CancellationToken cancellationToken) {
    var words = await wordRepository.GetWordsWithUserId(userId, cancellationToken);
    var result = words.Adapt<List<WordModel>>();

    return result;
  }

  public async Task<List<AuthResponseModel>> GetWordsByCategory(Guid userId, CancellationToken cancellationToken) {
    var user = await this.userRepository.GetUserWithRelations(userId, cancellationToken);

    var result = new List<AuthResponseModel>();
      user.Categories.ForEach(x => result.AddRange(x.Words.Select(w => new AuthResponseModel {
      Id = w.Id,
      Category = x.Value,
      Question = w.Question,
      Answers = w.Answers.Select(x => x.Value).ToArray(),
    })));

    return result;
  }
}
