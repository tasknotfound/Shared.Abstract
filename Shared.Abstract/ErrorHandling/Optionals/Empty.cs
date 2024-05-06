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

    public override Task<Optional<T>> WhenPresentAsync(Func<T, Task> action) => Task.FromResult<Optional<T>>(this);

    public override async Task<Optional<T>> WhenEmptyAsync(Func<Task> action)
    {
        await action();

        return this;
    }

    public override Task<Optional<U>> MapAsync<U>(Func<T, Task<U>> map)
        => Task.FromResult<Optional<U>>(new Empty<U>());

    public override Task<Optional<U>> ThenAsync<U>(Func<T, Task<Optional<U>>> map)
        => Task.FromResult<Optional<U>>(new Empty<U>());

    public override async Task<Optional<T>> OrAsync(Func<Task<Optional<T>>> map) => await map();

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
