using Words.Domain.Primitives;

namespace Words.Domain.Entities;

public class User : Entity {
  /// <summary>
  /// For DbContext
  /// </summary>
  private User(Guid id, string username, string email, string password, string passwordSalt) : base(id) {
    this.Username = username;
    this.Email = email;
    this.Password = password;
    this.PasswordSalt = passwordSalt;
  }

  private User(Guid id, string username, string email, string password, string passwordSalt, List<Category> categories) : base(id) {
    this.Username = username;
    this.Email = email;
    this.Password = password;
    this.PasswordSalt = passwordSalt;
    this.Categories = categories;
  }
  public string Username { get; private set; }

  public string Email { get; private set; }

  public string Password { get; private set; }

  public string PasswordSalt { get; set; }

  public virtual List<Category> Categories { get; private set; }

  public static User Create(string username, string email, string password, string passwordSalt, List<Category> categories) {
    return new User(Guid.NewGuid(), username, email, password, passwordSalt, categories);
  }

  /* public void AddCategory(Category category) { */
  /*   this.Categories.Add(category); */
  /* } */

  /* public List<Category> GetCategories() { */
  /*   return this.Categories; */
  /* } */

  /* public bool IsCategoryExist(string @value) { */
  /*   return this.Categories.Any(x => x.Value == @value); */
  /* } */
}
