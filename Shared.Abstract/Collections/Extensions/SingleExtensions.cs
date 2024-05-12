using TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

namespace TaskNotFound.Shared.Abstract.Collections.Extensions;

public static class SingleExtensions
{
    public static Optional<TSource> Single<TSource>(this IEnumerable<TSource> source)
        => Optional.OfNullable(source.SingleOrDefault());

    public static Optional<TSource> Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        => Optional.OfNullable(source.SingleOrDefault(predicate));
}
