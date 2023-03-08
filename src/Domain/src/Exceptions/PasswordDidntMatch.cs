namespace Words.Domain.Exceptions;

public sealed class PasswordDidntMatch : DomainException {
  public PasswordDidntMatch(string message) : base(message) {
  } 
}
