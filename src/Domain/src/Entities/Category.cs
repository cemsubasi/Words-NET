using Words.Domain.Primitives;

namespace Words.Domain.Entities;

public class Category : Entity {
  private Category(Guid id, string @value, Guid userId) : base(id) {
    this.Value = @value;
    this.UserId = userId;
  }

  private Category(Guid id, string @value, User user) : base(id) {
    this.Value = @value;
    this.User = user;
  }

  private Category(Guid id, string @value, List<Word> words, User user) : base(id) {
    this.Value = @value;
    this.Words = words;
    this.User = user;
  }

  public Guid UserId { get; set; }

  public virtual User User { get; set; }

  public string Value { get; private set; }

  public virtual List<Word> Words { get; private set; }

  public static Category Create(string @value, User user) {
    return new Category(id: Guid.NewGuid(), @value, user);
  }

  /* public static Category Create(string @value) { */
  /*   return new Category(id: Guid.NewGuid(), @value); */
  /* } */

  /* public void AddWords(Word word) { */
  /*   if (this.Words is not null) { */
  /*     this.Words.Add(word); */
  /*     return; */
  /*   } */

  /*   this.Words = new () { */
  /*     word */
  /*   }; */
  /* } */
}
