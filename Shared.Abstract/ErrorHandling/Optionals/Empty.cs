using TaskNotFound.Shared.Abstract.ErrorHandling.Exceptions;

namespace TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

internal sealed class Empty<T> : Optional<T>
{
    internal Empty()
    {
    }

    public override bool IsPresent => false;
    public override bool IsEmpty => true;

    public override T Unwrap() => throw new UnwrapException("Unwrap when value is Empty");
    public override T UnwrapOr(T @default) => @default;
    public override T UnwrapOrElse(Func<T> map) => map();
    public override T UnwrapOrElseThrow(Func<Exception> map) => throw map();

    public override Optional<T> WhenPresent(Action<T> action) => this;
    public override Optional<T> WhenEmpty(Action action)
    {
        action();

        return this;
    }

    public override Optional<U> Map<U>(Func<T, U> map) => new Empty<U>();
    public override Optional<U> Then<U>(Func<T, Optional<U>> map) => new Empty<U>();
    public override Optional<T> Or(Func<Optional<T>> map) => map();

    public override int GetHashCode() => 17;
    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }

        return obj is Empty<T>;
    }
}
