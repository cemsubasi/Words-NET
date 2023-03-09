using System.Text.Json.Serialization;

namespace Words.Domain.ValueObjects;

public class AuthResponseModel {
  [JsonIgnore]
  public Guid UserId { get; set; }
  public Guid Id { get; set; }
  public string? Category{ get; set; }
  public string? Question { get; set; }
  public string[]? Answers { get; set; }
}
