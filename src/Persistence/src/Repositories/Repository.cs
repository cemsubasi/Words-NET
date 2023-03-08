using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Words.Domain.Primitives;
using Words.Domain.Repositories;
using Words.Persistence.Contexts;

namespace Words.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity {
  private readonly MainDbContext mainDbContext;

  public Repository(MainDbContext mainDbContext) {
    this.mainDbContext = mainDbContext;
  }

  public void Add(TEntity entity) {
    this.mainDbContext.Set<TEntity>().Add(entity);
  }

  public void Update(TEntity entity) {
    this.mainDbContext.Set<TEntity>().Update(entity);
  }

  public IQueryable<TEntity> GetAll(bool asNoTracking) {
    if (asNoTracking) {
      return this.mainDbContext.Set<TEntity>().AsNoTracking().AsQueryable();
    }

    return this.mainDbContext.Set<TEntity>().AsQueryable();
  }

  public async Task<ICollection<TEntity>> GetAllAsync() {
    return await this.mainDbContext.Set<TEntity>().ToListAsync();
  }

  public Task<TEntity?> GetById(Guid id) {
    return this.mainDbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
  }

  public IQueryable<TEntity> GetQueryable() {
    return this.mainDbContext.Set<TEntity>();
  }

  public IQueryable<TEntity> GetQueryableById(Guid id) {
    return this.mainDbContext.Set<TEntity>().Where(x => x.Id == id);
  }

  public IEnumerable<TEntity> Where(Func<TEntity, bool> expression) {
    return this.mainDbContext.Set<TEntity>().Where(expression);
  }

  public void Remove(TEntity entity) {
    _ = this.mainDbContext.Set<TEntity>().Remove(entity);
  }

  public async Task AddRangeAsync(IEnumerable<TEntity> entities) {
    await this.mainDbContext.Set<TEntity>().AddRangeAsync(entities);
  }

  public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression) {
    return await this.mainDbContext.Set<TEntity>().Where(expression).ToListAsync();
  }
}
