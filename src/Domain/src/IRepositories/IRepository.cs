using System.Linq.Expressions;

using Words.Domain.Primitives;

namespace Words.Domain.Repositories;

public interface IRepository<T> where T: Entity {
  void Add(T entity);
  Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
  Task<T?> GetById(Guid id, CancellationToken cancellationToken);
  IQueryable<T> GetQueryable();
  IQueryable<T> GetQueryableById(Guid id);
  IQueryable<T> GetAll(bool asNoTracking);
  Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken);
  IEnumerable<T> Where(Func<T, bool> expression);
  Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
  void Update(T entity);
  void Remove(T entity);
  Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}
