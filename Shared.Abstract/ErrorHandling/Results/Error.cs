using TaskNotFound.Shared.Abstract.ErrorHandling.Exceptions;

namespace TaskNotFound.Shared.Abstract.ErrorHandling.Results;

internal sealed class Error<T, E> : Result<T, E>
{
    private readonly E _error;

    internal Error(E error) => _error = error;

    public override bool IsOk => false;
    public override bool IsError => true;

    public override T Unwrap() => throw new UnwrapException($"Unwrap when value is Error (error: {_error})");
    public override E UnwrapError() => _error;
    public override T UnwrapOr(T @default) => @default;
    public override T UnwrapOrElse(Func<E, T> map) => map(_error);
    public override T UnwrapOrElseThrow(Func<E, Exception> map) => throw map(_error);

    public override Result<T, E> WhenOk(Action<T> action) => this;
    public override Result<T, E> WhenError(Action<E> action)
    {
        action(_error);

        return this;
    }

    public override Result<U, E> Map<U>(Func<T, U> map) => new Error<U, E>(_error);
    public override Result<T, O> MapError<O>(Func<E, O> map) => new Error<T, O>(map(_error));

    public override Result<U, E> Then<U>(Func<T, Result<U, E>> map) => new Error<U, E>(_error);
    public override Result<T, O> Or<O>(Func<E, Result<T, O>> map) => map(_error);

    public override int GetHashCode() => _error is not null ? _error.GetHashCode() * 17 : 17;
    public override bool Equals(object? obj)
    {
        if (this == obj)
        {
            return true;
        }

        if (obj is not Error<T, E> other)
        {
            return false;
        }

        if (_error is null)
        {
            return false;
        }

        return _error.Equals(other._error);
    }
}
