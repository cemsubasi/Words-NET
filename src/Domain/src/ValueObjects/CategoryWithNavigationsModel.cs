namespace Words.Domain.ValueObjects;

public class CategoryWithNavigationsModel {
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public UserModel User { get; set; }
  public string Value { get; set; }
  public List<WordModel> Words { get; set; }
}
