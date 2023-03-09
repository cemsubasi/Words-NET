using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Words.Domain.Entities;
using Words.Infastructure.Authentication;

namespace Words.Persistence.Contexts;

public class MainDbContext : DbContext {
  public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {
  }

  public DbSet<User> Users { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Word> Words { get; set; }
  public DbSet<Answer> Answers { get; set; }
}
