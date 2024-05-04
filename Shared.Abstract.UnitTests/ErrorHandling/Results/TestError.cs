namespace TaskNotFound.Shared.Abstract.ErrorHandling.Results;

public class TestError : IEquatable<TestError>
{
    public bool Equals(TestError? other) => this == other;
}
