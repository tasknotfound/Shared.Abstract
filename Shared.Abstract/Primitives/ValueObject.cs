namespace TaskNotFound.Shared.Abstract.Primitives;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        if (left == null ^ right == null) return false;
        return left?.Equals(right) != false;
    }

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right) => !EqualOperator(left, right);

    public abstract IEnumerable<object?> GetAtomicValues();

    public override bool Equals(object? obj) => obj is ValueObject valueObject &&
                                                GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());

    public override int GetHashCode() =>
        GetAtomicValues()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);

    public static bool operator ==(ValueObject? left, ValueObject? right) => EqualOperator(left, right);

    public static bool operator !=(ValueObject? left, ValueObject? right) => NotEqualOperator(left, right);
}