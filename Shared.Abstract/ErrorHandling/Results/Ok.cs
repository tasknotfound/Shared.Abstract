using TaskNotFound.Shared.Abstract.ErrorHandling.Exceptions;

namespace TaskNotFound.Shared.Abstract.ErrorHandling.Results;

internal sealed class Ok<T, E> : Result<T, E>
{
    private readonly T _value;

    internal Ok(T value) => _value = value;

    public override bool IsOk => true;
    public override bool IsError => false;

    public override T Unwrap() => _value;

    public override E UnwrapError() => throw new UnwrapException($"UnwrapError when value is Ok (value: {_value})");

    public override T UnwrapOr(T @default) => _value;

    public override T UnwrapOrElse(Func<E, T> map) => _value;

    public override T UnwrapOrElseThrow(Func<E, Exception> map) => _value;

    public override Result<T, E> WhenError(Action<E> action) => this;

    public override Result<T, E> WhenOk(Action<T> action)
    {
        action(_value);

        return this;
    }

    public override Result<U, E> Map<U>(Func<T, U> map) => new Ok<U, E>(map(_value));

    public override Result<T, O> MapError<O>(Func<E, O> map) => new Ok<T, O>(_value);

    public override Result<U, E> Then<U>(Func<T, Result<U, E>> map) => map(_value);

    public override Result<T, O> Or<O>(Func<E, Result<T, O>> map) => new Ok<T, O>(_value);

    public override Task<Result<T, E>> WhenErrorAsync(Func<E, Task> action) => Task.FromResult<Result<T, E>>(this);

    public override async Task<Result<T, E>> WhenOkAsync(Func<T, Task> action)
    {
        await action(_value);

        return this;
    }

    public override async Task<Result<U, E>> MapAsync<U>(Func<T, Task<U>> map)
        => new Ok<U, E>(await map(_value));

    public override Task<Result<T, O>> MapErrorAsync<O>(Func<E, Task<O>> map)
        => Task.FromResult<Result<T, O>>(new Ok<T, O>(_value));

    public override async Task<Result<U, E>> ThenAsync<U>(Func<T, Task<Result<U, E>>> map)
        => await map(_value);

    public override Task<Result<T, O>> OrAsync<O>(Func<E, Task<Result<T, O>>> map)
        => Task.FromResult<Result<T, O>>(new Ok<T, O>(_value));

    public override int GetHashCode() => _value is not null ? _value.GetHashCode() * 17 : 17;

    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj is not Ok<T, E> other)
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
