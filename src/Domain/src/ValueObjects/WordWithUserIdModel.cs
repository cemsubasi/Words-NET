namespace Words.Domain.ValueObjects;

public record WordWithUserIdModel(Guid UserId, string Question, IEnumerable<AnswerModel> Answers, Guid CategoryId) : WordModel(Question,  Answers, CategoryId); 
