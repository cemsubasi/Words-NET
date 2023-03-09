using System.Security.Cryptography;

namespace Words.Application.Helpers;

public class PasswordManager : IPasswordManager {
  private const string letters = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789";

  public string GenerateSalt(int digit = 8) {
    var hash = string.Empty;

    for (int i = 0; i < digit; i++) {
      var rnd = Random.Shared.Next(0, letters.Length);
      hash += letters.AsEnumerable().ElementAt(rnd);
    }

    return hash;
  }

  public string GenerateHash(string password, string passwordSalt) {
    byte[] data = System.Text.Encoding.UTF8.GetBytes(password+passwordSalt);
    byte[] hash = SHA256.Create().ComputeHash(data);
    var result = Convert.ToBase64String(hash);

    return result;
  }
}
