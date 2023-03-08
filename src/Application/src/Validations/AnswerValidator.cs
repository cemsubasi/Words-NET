using FluentValidation;

using Words.Domain.ValueObjects;

namespace Words.Application.Validations;

public class AnswerValidator : AbstractValidator<AnswerModel> {
  public AnswerValidator() {
    RuleFor(x => x.Value)
      .NotNull()
      .NotEmpty()
      .Length(2, 50);
  }
}
