namespace TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

public class TestValue : IEquatable<TestValue>
{
    public bool Equals(TestValue? other) => this == other;
}
