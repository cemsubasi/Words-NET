namespace Words.Application.Helpers;

public interface IPasswordManager {
	string GenerateSalt(int digit);
	string GenerateHash(string password, string passwordHash);
}
