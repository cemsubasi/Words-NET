using FluentValidation;

using Words.Domain.ValueObjects;

namespace Words.Application.Validations;

public class CategoryValidator : AbstractValidator<CategoryModel> {
  public CategoryValidator() {
    RuleFor(x => x.Value)
      .NotNull()
      .NotEmpty()
      .Length(3, 50);
  }
}
