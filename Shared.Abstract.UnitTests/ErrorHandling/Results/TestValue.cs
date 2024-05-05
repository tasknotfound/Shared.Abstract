namespace TaskNotFound.Shared.Abstract.ErrorHandling.Results;

public class TestValue : IEquatable<TestValue>
{
    public bool Equals(TestValue? other) => this == other;
}
