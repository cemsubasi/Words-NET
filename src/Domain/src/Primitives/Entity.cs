namespace Words.Domain.Primitives;

public abstract class Entity : IEquatable<Entity> {
  protected Entity(Guid id) {
    this.Id = id;
  }

  public Guid Id { get; private init; }

  public static bool operator ==(Entity? first, Entity? second) {
    return first is not null && second is not null && first.Equals(second);
  }

  public static bool operator !=(Entity? first, Entity? second) {
    return !(first == second);
  }

  public override bool Equals(object? obj) {
    if (obj is null) {
      return false;
    }

    if (obj.GetType() != GetType()) {
      return false;
    }

    if (obj is not Entity entity) {
      return false;
    }

    return entity.Id == this.Id;
  }

  public bool Equals(Entity? entity) {
    if (entity is null) {
      return false;
    }
    return entity.Id == this.Id;
  }

  public override int GetHashCode() {
    return this.Id.GetHashCode();
  }
}
