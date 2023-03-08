using Words.Domain.Primitives;

namespace Words.Domain.Entities;

public class Word : Entity {
  private Word(Guid id, string question) : base(id) {
    this.Question = question;
  }

  private Word(Guid id, string question, Category category) : base(id) {
    this.Question = question;
    this.Category = category;
  }

  private Word(Guid id, string question, Category category, List<Answer> answers) : base(id) {
    this.Question = question;
    this.Answers = answers;
    this.Category = category;
  }

  public string Question { get; private set; }

  public virtual List<Answer> Answers { get; private set; }

  public Guid CategoryId { get; private set; }

  public virtual Category Category { get; private set; }

  public static Word Create(string question, Category category) {
    return new Word(id: Guid.NewGuid(), question: question, category: category);
  }

  public static Word Create(string question, Category category, List<Answer> answers) {
    return new Word(id: Guid.NewGuid(), question: question, category: category, answers: answers);
  }

  /* public void AddAnswers(List<Answer> answers) { */
  /*   this.Answers = answers; */
  /* } */
}
