using Words.Domain.Entities;

namespace Words.Domain.Repositories;

public interface IWordRepository: IRepository<Word> {
  Task<List<Word>> GetWordsWithUserId(Guid id, CancellationToken cancellationToken);
}
