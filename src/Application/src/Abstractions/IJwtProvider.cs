using Words.Domain.Entities;

namespace Words.Application.Abstractions;

public interface IJwtProvider {
	string Generate(User user);
}
