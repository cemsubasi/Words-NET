using FluentValidation;

using Words.Domain.ValueObjects;

namespace Words.Application.Validations;

public class UserValidator : AbstractValidator<UserModel> {
   public UserValidator() {
     RuleFor(x => x.Username)
       .NotNull()
       .NotEmpty()
       .Length(3, 30);

     RuleFor(x => x.Email)
       .NotNull()
       .NotEmpty()
       .EmailAddress();

     RuleFor(x => x.Password)
       .NotNull()
       .NotEmpty()
       .Length(3, 30);
  }
}
