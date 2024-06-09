using TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;

namespace TaskNotFound.Shared.Abstract.Collections.Extensions;

public static class ElementAtExtensions
{
    public static Optional<TSource> ElementAt<TSource>(this IEnumerable<TSource> source, Index index)
        => Optional.OfNullable(source.ElementAtOrDefault(index));

    public static Optional<TSource> ElementAt<TSource>(this IEnumerable<TSource> source, int index)
        => Optional.OfNullable(source.ElementAtOrDefault(index));
}
