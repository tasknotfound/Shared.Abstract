namespace TaskNotFound.Shared.Abstract.ErrorHandling.Results;

public class Result
{
    public static Result<T, E> Ok<T, E>(T value) => new Ok<T, E>(value);
    public static Result<T, E> Error<T, E>(E error) => new Error<T, E>(error);
}

public abstract class Result<T, E>
{
    public abstract bool IsOk { get; }
    public abstract bool IsError { get; }

    public abstract T Unwrap();
    public abstract E UnwrapError();
    public abstract T UnwrapOr(T @default);
    public abstract T UnwrapOrElse(Func<E, T> map);
    public abstract T UnwrapOrElseThrow(Func<E, Exception> map);

    public abstract Result<T, E> WhenOk(Action<T> action);
    public abstract Result<T, E> WhenError(Action<E> action);

    public abstract Result<U, E> Map<U>(Func<T, U> map);
    public abstract Result<T, O> MapError<O>(Func<E, O> map);

    public abstract Result<U, E> Then<U>(Func<T, Result<U, E>> map);
    public abstract Result<T, O> Or<O>(Func<E, Result<T, O>> map);
}
