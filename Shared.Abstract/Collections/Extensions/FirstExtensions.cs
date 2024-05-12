using TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

namespace TaskNotFound.Shared.Abstract.Collections.Extensions;

public static class FirstExtensions
{
    public static Optional<TSource> First<TSource>(this IEnumerable<TSource> source)
        => Optional.OfNullable(source.FirstOrDefault());

    public static Optional<TSource> First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        => Optional.OfNullable(source.FirstOrDefault(predicate));
}
