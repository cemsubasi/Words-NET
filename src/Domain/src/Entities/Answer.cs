using Words.Domain.Primitives;

namespace Words.Domain.Entities;

public class Answer : Entity {
  private Answer(Guid id, string @value) : base(id) {
    this.Value = @value;
  }

  private Answer(Guid id, string @value, Word word) : base(id) {
    this.Value = @value;
    this.Word = word;
  }

  public string Value { get; private set; }

  public Guid WordId { get; set; }

  public virtual Word Word { get; set; }

  public static Answer Create(string @value, Word word) {
    return new Answer(id: Guid.NewGuid(), @value, word);
  }
}
