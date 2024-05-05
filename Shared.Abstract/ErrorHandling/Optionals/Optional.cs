namespace TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

public class Optional
{
    public static Optional<T> Of<T>(T value) => new Present<T>(value);
    public static Optional<T> Empty<T>() => new Empty<T>();
    public static Optional<T> OfNullable<T>(T? value) => value is not null ? new Present<T>(value) : new Empty<T>();
}

public abstract class Optional<T>
{
    public abstract bool IsPresent { get; }
    public abstract bool IsEmpty { get; }

    public abstract T Unwrap();
    public abstract T UnwrapOr(T @default);
    public abstract T UnwrapOrElse(Func<T> map);
    public abstract T UnwrapOrElseThrow(Func<Exception> map);

    public abstract Optional<T> WhenPresent(Action<T> action);
    public abstract Optional<T> WhenEmpty(Action action);

    public abstract Optional<U> Map<U>(Func<T, U> map);
    public abstract Optional<U> Then<U>(Func<T, Optional<U>> map);
    public abstract Optional<T> Or(Func<Optional<T>> map);
}
