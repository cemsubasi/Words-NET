using Words.Domain.Primitives;

namespace Words.Domain.Entities;

public class User : Entity {
  /* private User(Guid id, string username, string email, string password, IEnumerable<Word> words) : base(id) { */
  /*   this.Words = words; */
  /*   User(id, username,email, password); */
  /* } */

  private User(Guid id, string username, string email, string password) : base(id) {
    this.Username = username;
    this.Email = email;
    this.Password = password;
  }

  private User(Guid id, string username, string email, string password, List<Category> categories) : base(id) {
    this.Username = username;
    this.Email = email;
    this.Password = password;
    this.Categories = categories;
  }
  public string Username { get; private set; }

  public string Email { get; private set; }

  public string Password { get; private set; }

  public virtual List<Category> Categories { get; private set; }

  public static User Create(string username, string email, string password, List<Category> categories) {
    return new User(Guid.NewGuid(), username, email, password, categories);
  }

  public void AddCategory(Category category) {
    this.Categories.Add(category);
  }

  public List<Category> GetCategories() {
    return this.Categories;
  }

  public bool IsCategoryExist(string @value) {
    return this.Categories.Any(x => x.Value == @value);
  }
}
