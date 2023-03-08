namespace Words.Domain.ValueObjects;

public record WordModel (string Question, IEnumerable<AnswerModel> Answers, Guid CategoryId);
/* { */
/*   public Guid UserId { get; set; } */
/*   public string Question { get; set; } */
/*   public IEnumerable<AnswerModel> Answers { get; set; } */
/*   public Guid CategoryId { get; set; } */
/* } */
