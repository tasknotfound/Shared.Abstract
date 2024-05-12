using TaskNotFound.Shared.Abstract.ErrorHandling.Optionals;
using TaskNotFound.Shared.Abstract.ErrorHandling.Results;

namespace TaskNotFound.Shared.Abstract.Collections.Extensions;

public static class ToListExtensions
{
    public static Optional<List<TSource>> ToList<TSource>(this IEnumerable<Optional<TSource>> source)
    {
        var list = new List<TSource>();

        foreach (var option in source)
        {
            if (option.IsEmpty)
            {
                return Optional.Empty<List<TSource>>();
            }

            list.Add(option.Unwrap());
        }

        return Optional.Of(list);
    }

    public static Result<List<TSource>, TError> ToList<TSource, TError>(
        this IEnumerable<Result<TSource, TError>> source)
    {
        var list = new List<TSource>();

        foreach (var result in source)
        {
            if (result.IsError)
            {
                return Result.Error<List<TSource>, TError>(result.UnwrapError());
            }

            list.Add(result.Unwrap());
        }

        return Result.Ok<List<TSource>, TError>(list);
    }
}
