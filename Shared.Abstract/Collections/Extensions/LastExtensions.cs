using TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

namespace TaskNotFound.Shared.Abstract.Collections.Extensions;

public static class LastExtensions
{
    public static Optional<TSource> Last<TSource>(this IEnumerable<TSource> source)
        => Optional.OfNullable(source.LastOrDefault());

    public static Optional<TSource> Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        => Optional.OfNullable(source.LastOrDefault(predicate));
}
