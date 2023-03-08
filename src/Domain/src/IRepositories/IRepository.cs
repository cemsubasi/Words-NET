using System.Linq.Expressions;

using Words.Domain.Primitives;

namespace Words.Domain.Repositories;

public interface IRepository<T> where T: Entity {
  void Add(T entity);
  Task AddRangeAsync(IEnumerable<T> entities);
  Task<T?> GetById(Guid id);
  IQueryable<T> GetQueryable();
  IQueryable<T> GetQueryableById(Guid id);
  IQueryable<T> GetAll(bool asNoTracking);
  Task<ICollection<T>> GetAllAsync();
  IEnumerable<T> Where(Func<T, bool> expression);
  Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> expression);
  void Update(T entity);
  void Remove(T entity);
}
