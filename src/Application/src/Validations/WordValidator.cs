using FluentValidation;

using Words.Domain.ValueObjects;

namespace Words.Application.Validations;

public class WordValidator : AbstractValidator<WordModel> {
  public WordValidator() {
    RuleFor(x => x.Question)
      .NotNull()
      .NotEmpty()
      .Length(2, 50);

    RuleFor(x => x.Answers)
      .ForEach(x => x.SetValidator(new AnswerValidator()));
  }
}
