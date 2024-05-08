namespace TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

internal sealed class Present<T> : Optional<T>
{
    private readonly T _value;

    internal Present(T value) => _value = value;

    public override bool IsPresent => true;
    public override bool IsEmpty => false;

    public override T Unwrap() => _value;

    public override T UnwrapOr(T @default) => _value;

    public override T UnwrapOrElse(Func<T> map) => _value;

    public override T UnwrapOrElseThrow(Func<Exception> map) => _value;

    public override Optional<T> WhenEmpty(Action action) => this;

    public override Optional<T> WhenPresent(Action<T> action)
    {
        action(_value);

        return this;
    }

    public override Optional<U> Map<U>(Func<T, U> map) => new Present<U>(map(_value));

    public override Optional<U> Then<U>(Func<T, Optional<U>> map) => map(_value);

    public override Optional<T> Or(Func<Optional<T>> map) => new Present<T>(_value);

    public override Task<Optional<T>> WhenEmptyAsync(Func<Task> action) => Task.FromResult<Optional<T>>(this);

    public override async Task<Optional<T>> WhenPresentAsync(Func<T, Task> action)
    {
        await action(_value);

        return this;
    }

    public override async Task<Optional<U>> MapAsync<U>(Func<T, Task<U>> map) => new Present<U>(await map(_value));

    public override async Task<Optional<U>> ThenAsync<U>(Func<T, Task<Optional<U>>> map) => await map(_value);

    public override Task<Optional<T>> OrAsync(Func<Task<Optional<T>>> map)
        => Task.FromResult<Optional<T>>(new Present<T>(_value));

    public override int GetHashCode() => _value is not null ? _value.GetHashCode() * 17 : 17;

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj is not Present<T> other)
        {
            return false;
        }

        if (_value is null)
        {
            return false;
        }

        return _value.Equals(other._value);
    }
}
