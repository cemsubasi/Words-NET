using Microsoft.EntityFrameworkCore;

using Words.Domain.Entities;

namespace Words.Persistence.Contexts;

public class MainDbContext : DbContext {
  private const string ConnectionString = "server=localhost;database=words-clean-arch;user=root;password=parola";

  protected override void OnConfiguring(DbContextOptionsBuilder options) {
    _ = options.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 28)));
  }

  public DbSet<User> Users { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Word> Words { get; set; }
  public DbSet<Answer> Answers { get; set; }
}
